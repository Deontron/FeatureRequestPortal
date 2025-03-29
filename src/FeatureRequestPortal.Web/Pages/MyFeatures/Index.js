$(function () {
    var l = abp.localization.getResource('FeatureRequestPortal');

    var dataTable = $('#MyFeaturesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(featureRequestPortal.myFeatures.myFeature.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'FeatureDeletionConfirmationMessage',
                                            data.record.title
                                        );
                                    },
                                    action: function (data) {
                                        featureRequestPortal.myFeatures.myFeature
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Başlık'),
                    data: "title"
                },
                {
                    title: l('Kategori'),
                    data: "category",
                    render: function (data) {
                        return l('Enum:MyFeatureCategory.' + data);
                    }
                },
                {
                    title: l('Tarih'),
                    data: "publishDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                },
                {
                    title: l('Açıklama'),
                    data: "description"
                },
                {
                    title: l('Oluşturulma Zamanı'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'MyFeatures/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'MyFeatures/EditModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewFeatureButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
