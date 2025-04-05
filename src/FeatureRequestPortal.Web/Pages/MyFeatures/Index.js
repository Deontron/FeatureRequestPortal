$(function () {
    loadFeatures();

    async function loadFeatures() {
        try {
            let data = await abp.ajax({
                url: abp.appPath + 'api/app/my-feature',
                type: 'GET'
            });
            renderFeatures(data.items.filter(feature => feature.isApproved));
        } catch (error) {
            console.error("Veri çekme hatası:", error);
        }
    }

    async function renderFeatures(features) {
        let container = $("#featureContainer");
        container.empty();

        if (!Array.isArray(features) || features.length === 0) {
            container.append("<p class='text-muted'>Henüz özellik eklenmemiş.</p>");
            return;
        }

        for (let feature of features) {
            let likeClass = 'btn-outline-success';
            let dislikeClass = 'btn-outline-danger';

            try {
                let response = await abp.ajax({
                    url: abp.appPath + 'api/app/my-feature/user-score',
                    type: 'GET',
                    data: { featureId: feature.id }
                });

                if (response.scoreType === 'like') {
                    likeClass = 'btn-success';
                } else if (response.scoreType === 'dislike') {
                    dislikeClass = 'btn-danger';
                }
            } catch (error) {
                console.error("Puan durumu çekilirken hata:", error);
                likeClass = 'btn-outline-success';
                dislikeClass = 'btn-outline-danger';
            }

            let categoryText = localizeCategory(feature.category);

            let cardHtml = `
                <div class="col-md-6 col-lg-4">
                    <div class="card mb-3 shadow-sm">
                        <div class="card-header">
                            <h5 class="card-title">${feature.title}</h5>
                            <small class="text-muted">${new Date(feature.creationTime).toLocaleDateString()}</small>
                        </div>
                        <div class="card-body">
                            <p>${feature.description}</p>
                        </div>
                        <div class="card-footer text-end">
                            <p>Kategori: ${categoryText}</p>
                            <button class="btn ${likeClass} like-btn" data-id="${feature.id}">
                                <i class="fas fa-thumbs-up"></i>
                            </button>
                            <button class="btn ${dislikeClass} dislike-btn" data-id="${feature.id}">
                                <i class="fas fa-thumbs-down"></i>
                            </button>
                            <span class="score">${feature.point}</span> 
                            <button class="btn btn-primary details-btn" 
                                data-id="${feature.id}" 
                                data-title="${feature.title}" 
                                data-description="${feature.description}"
                                data-category="${feature.category}"
                                data-creator="${feature.creatorId}"
                                data-date="${new Date(feature.creationTime).toLocaleDateString()}"
                                data-point="${feature.point}">
                                Detaylar
                            </button>
                        </div>
                    </div>
                </div>`;

            container.append(cardHtml);
        }

        $(document).on("click", ".like-btn", function () {
            let featureId = $(this).data("id");
            updateFeaturePoint(featureId, "like");
        });

        $(document).on("click", ".dislike-btn", function () {
            let featureId = $(this).data("id");
            updateFeaturePoint(featureId, "dislike");
        });

        $(document).on("click", ".details-btn", async function () {
            let featureId = $(this).data("id");
            let featureTitle = $(this).data("title");
            let featureDescription = $(this).data("description");
            let featureCreator = $(this).data("creator");
            let featureCreationDate = $(this).data("date");
            let featureCategory = $(this).data("category");

            $("#featureTitle")
                .text(featureTitle)
                .data("id", featureId);

            $("#featureDescription").text(featureDescription);
            $("#featureCreator").text(featureCreator);
            $("#featureCreationDate").text(featureCreationDate);
            $("#featureCategory").text(localizeCategory(featureCategory));

            try {
                let response = await abp.ajax({
                    url: abp.appPath + `api/app/my-feature/${featureId}`,
                    type: 'GET'
                });
                $("#featurePoint").text(response.point);
            } catch (error) {
                console.error("Puan çekilirken hata:", error);
            }

            let currentUser = abp.currentUser;
            if (currentUser.id === featureCreator || currentUser.roles.includes('admin')) {
                $("#EditFeatureButton").show();
                $("#DeleteFeatureButton").show();
            } else {
                $("#EditFeatureButton").hide();
                $("#DeleteFeatureButton").hide();
            }

            $("#featureDetailsModal").modal("show");
        });

        disableButtonsForGuests();
    }

    var createModal = new abp.ModalManager(abp.appPath + 'MyFeatures/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'MyFeatures/EditModal');

    createModal.onResult(function () {
        loadFeatures();
    });

    editModal.onResult(function () {
        loadFeatures();
    });

    $(document).on("click", "#NewFeatureButton", function (e) {
        e.preventDefault();
        createModal.open();
    });

    $(document).on("click", "#EditFeatureButton", function (e) {
        e.preventDefault();
        let featureId = $("#featureTitle").data("id");
        editModal.open({ id: featureId });
    });

    $(document).on("click", "#DeleteFeatureButton", async function (e) {
        e.preventDefault();
        let featureId = $("#featureTitle").data("id");
        abp.message.confirm(
            abp.localization.localize('FeatureDeletionConfirmationMessage', 'FeatureRequestPortal', $("#featureTitle").text()),
            function (isConfirmed) {
                if (isConfirmed) {
                    featureRequestPortal.myFeatures.myFeature
                        .delete(featureId)
                        .then(function () {
                            abp.notify.info(abp.localization.localize('SuccessfullyDeleted', 'FeatureRequestPortal'));
                            $("#featureDetailsModal").modal("hide");
                            loadFeatures();
                        });
                }
            }
        );
    });

    function disableButtonsForGuests() {
        if (!abp.currentUser.isAuthenticated) {
            $(".like-btn, .dislike-btn").attr("disabled", true);
        }
    }

    async function updateFeaturePoint(featureId, changeType) {
        try {
            let response = await abp.ajax({
                url: abp.appPath + 'api/app/my-feature/update-score',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({
                    featureId: featureId,
                    scoreType: changeType,
                    userId: abp.currentUser.userId
                })
            });
            $(`button[data-id="${featureId}"]`).siblings(".score").text(response.point);

            let likeButton = $(`button.like-btn[data-id="${featureId}"]`);
            let dislikeButton = $(`button.dislike-btn[data-id="${featureId}"]`);

            if (changeType === "like") {
                if (likeButton.hasClass("btn-success")) {
                    likeButton.removeClass("btn-success").addClass("btn-outline-success");
                } else {
                    likeButton.removeClass("btn-outline-success").addClass("btn-success");
                    dislikeButton.removeClass("btn-danger").addClass("btn-outline-danger");
                }
            } else if (changeType === "dislike") {
                if (dislikeButton.hasClass("btn-danger")) {
                    dislikeButton.removeClass("btn-danger").addClass("btn-outline-danger");
                } else {
                    dislikeButton.removeClass("btn-outline-danger").addClass("btn-danger");
                    likeButton.removeClass("btn-success").addClass("btn-outline-success");
                }
            }

            let currentModalFeatureId = $("#featureTitle").data("id");
            if (currentModalFeatureId === featureId) {
                try {
                    let updatedFeature = await abp.ajax({
                        url: abp.appPath + `api/app/my-feature/${featureId}`,
                        type: 'GET'
                    });
                    $("#featurePoint").text(updatedFeature.point);
                } catch (error) {
                    console.error("Puan çekilirken hata:", error);
                }
            }
        } catch (error) {
            console.error("Puan güncellenirken hata:", error);
        }
    }

    function localizeCategory(category) {
        return abp.localization.localize('Enum:MyFeatureCategory.' + category, 'FeatureRequestPortal');
    }
});

