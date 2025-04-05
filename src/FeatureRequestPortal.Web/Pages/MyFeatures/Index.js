$(function () {
    loadFeatures();

    function loadFeatures() {
        abp.ajax({
            url: abp.appPath + 'api/app/my-feature',
            type: 'GET'
        }).done(function (data) {
            let features = data.items;
            renderFeatures(features);
        }).fail(function (error) {
            console.error("Veri çekme hatası:", error);
        });
    }

    function renderFeatures(features) {
        let container = $("#featureContainer");
        container.empty();

        if (!Array.isArray(features) || features.length === 0) {
            container.append("<p class='text-muted'>Henüz özellik eklenmemiş.</p>");
            return;
        }

        features.forEach(feature => {
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
                    <p>Yaratıcı: ${feature.id}</p>
                    <button class="btn btn-outline-success like-btn" data-id="${feature.id}">
                        <i class="fas fa-thumbs-up"></i>
                    </button>
                    <button class="btn btn-outline-danger dislike-btn" data-id="${feature.id}">
                        <i class="fas fa-thumbs-down"></i>
                    </button>
                    <span class="score">${feature.point}</span> 
                    <button class="btn btn-primary details-btn" 
                        data-id="${feature.id}" 
                        data-title="${feature.title}" 
                        data-description="${feature.description}"
                        data-creator="${feature.id}"
                        data-date="${new Date(feature.creationTime).toLocaleDateString()}"
                        data-point="${feature.point}">
                        Detaylar
                    </button>
                </div>
            </div>
        </div>`;

            container.append(cardHtml);
        });

        $(".like-btn").click(function () {
            let featureId = $(this).data("id");

            if ($(this).hasClass("btn-success")) {
                $(this).removeClass("btn-success").addClass("btn-outline-success");
                updateFeaturePoint(featureId, "dislike");
            } else {
                $(this).removeClass("btn-outline-success").addClass("btn-success");
                $(this).siblings(".dislike-btn").removeClass("btn-danger").addClass("btn-outline-danger");
                updateFeaturePoint(featureId, "like");
            }
        });

        $(".dislike-btn").click(function () {
            let featureId = $(this).data("id");

            if ($(this).hasClass("btn-danger")) {
                $(this).removeClass("btn-danger").addClass("btn-outline-danger");
                updateFeaturePoint(featureId, "like");
            } else {
                $(this).removeClass("btn-outline-danger").addClass("btn-danger");
                $(this).siblings(".like-btn").removeClass("btn-success").addClass("btn-outline-success");
                updateFeaturePoint(featureId, "dislike");
            }
        });

        $(document).on("click", ".details-btn", function () {
            let featureId = $(this).data("id");
            let featureTitle = $(this).data("title");
            let featureDescription = $(this).data("description");
            let featureCreator = $(this).data("creator");
            let featureCreationDate = $(this).data("date");
            let featurePoint = $(this).data("point");

            $("#featureTitle")
                .text(featureTitle)
                .data("id", featureId); 

            $("#featureDescription").text(featureDescription);
            $("#featureCreator").text(featureCreator);
            $("#featureCreationDate").text(featureCreationDate);
            $("#featurePoint").text(featurePoint);

            $("#featureDetailsModal").modal("show");
        });

        disableButtonsForGuests();
    }

    var createModal = new abp.ModalManager(abp.appPath + 'MyFeatures/CreateModal');

    createModal.onResult(function () {
        loadFeatures();
    });

    $('#NewFeatureButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    function disableButtonsForGuests() {
        if (!abp.currentUser.isAuthenticated) {
            $(".like-btn, .dislike-btn").attr("disabled", true);
        }
    }

    function updateFeaturePoint(featureId, changeType) {
        abp.ajax({
            url: abp.appPath + 'api/app/my-feature/update-score',
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({
                featureId: featureId,
                scoreType: changeType
            })
        }).done(function (response) {
            console.log(response.message);

            $(`button[data-id="${featureId}"]`).siblings(".score").text(response.point);

            let currentModalFeatureId = $("#featureTitle").data("id");
            if (currentModalFeatureId === featureId) {
                $("#featurePoint").text(response.point);
            }
        }).fail(function (error) {
            console.error("Puan güncellenirken hata:", error);
        });
    }
});
