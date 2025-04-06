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
                                    visible: abp.auth.isGranted('FeatureRequestPortal.MyFeatures.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible: abp.auth.isGranted('FeatureRequestPortal.MyFeatures.Delete'),
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
                                },
                                {
                                    text: l('Approve'),
                                    visible: abp.auth.isGranted('FeatureRequestPortal.MyFeatures.Approve'),
                                    action: function (data) {
                                        featureRequestPortal.myFeatures.myFeature
                                            .approve({
                                                featureId: data.record.id,
                                                isApproved: true
                                            })
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyApproved'));
                                                dataTable.ajax.reload();
                                            }).catch(function (error) {
                                                console.error("Onaylanırken hata:", error);
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
                },
                {
                    title: l('Onay Durumu'),
                    data: "isApproved",
                    render: function (data) {
                        return data ? l('Approved') : l('Not Approved');
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

    $(document).on("click", "#NewFeatureButton", function (e) {
        e.preventDefault();
        createModal.open();
    });
});