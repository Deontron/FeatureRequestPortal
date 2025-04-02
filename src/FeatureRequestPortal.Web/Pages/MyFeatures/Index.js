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
                        <span class="score ms-2">0</span>
                        <button class="btn btn-outline-success like-btn" data-id="${feature.id}">👍</button>
                        <button class="btn btn-outline-danger dislike-btn" data-id="${feature.id}">👎</button>
                        <button class="btn btn-primary">Detaylar</button>
                    </div>
                </div>
            </div>`;

            container.append(cardHtml);
        });

        // Like butonuna tıklanınca puanı artır
        $(".like-btn").click(function () {
            let scoreSpan = $(this).siblings(".score");
            let currentScore = parseInt(scoreSpan.text());

            if ($(this).hasClass("btn-success")) {
                // Zaten like verilmişse geri al
                scoreSpan.text(currentScore - 1);
                $(this).removeClass("btn-success").addClass("btn-outline-success");
            } else {
                // Yeni like veriliyorsa
                scoreSpan.text(currentScore + 1);
                $(this).removeClass("btn-outline-success").addClass("btn-success");
                $(this).next(".dislike-btn").removeClass("btn-danger").addClass("btn-outline-danger"); // Dislike'ı sıfırla
            }
        });

        // Dislike butonuna tıklanınca puanı düşür
        $(".dislike-btn").click(function () {
            let scoreSpan = $(this).siblings(".score");
            let currentScore = parseInt(scoreSpan.text());

            if ($(this).hasClass("btn-danger")) {
                // Zaten dislike verilmişse geri al
                scoreSpan.text(currentScore + 1);
                $(this).removeClass("btn-danger").addClass("btn-outline-danger");
            } else {
                // Yeni dislike veriliyorsa
                scoreSpan.text(currentScore - 1);
                $(this).removeClass("btn-outline-danger").addClass("btn-danger");
                $(this).prev(".like-btn").removeClass("btn-success").addClass("btn-outline-success"); // Like'ı sıfırla
            }
        });
    }


    var createModal = new abp.ModalManager(abp.appPath + 'MyFeatures/CreateModal');

    createModal.onResult(function () {
        loadFeatures();
    });

    $('#NewFeatureButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
