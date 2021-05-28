(function () {
    const l = abp.localization.getResource('MasterData');
    const _companyAppService = masterData.companies.company;

    const _editModal = new abp.ModalManager(
        abp.appPath + 'MasterData/Companies/EditModal'
    );

    const _createModal = new abp.ModalManager(
        abp.appPath + 'MasterData/Companies/CreateModal'
    );

    let _dataTable = null;

    abp.ui.extensions.entityActions.get('masterData.company').addContributor(
        function(actionList) {
            return actionList.addManyTail(
                [
                    {
                        text: l('Edit'),
                        visible: abp.auth.isGranted(
                            'MasterData.Companies.Update'
                        ),
                        action: function (data) {
                            _editModal.open({
                                id: data.record.id,
                            });
                        },
                    },
                    {
                        text: l('Delete'),
                        visible: abp.auth.isGranted(
                            'MasterData.Companies.Delete'
                        ),
                        confirmMessage: function (data) {
                            return l(
                                'CompanyDeletionConfirmationMessage',
                                data.record.name
                            );
                        },
                        action: function (data) {
                            _companyAppService
                                .delete(data.record.id)
                                .then(function () {
                                    _dataTable.ajax.reload();
                                });
                        },
                    }
                ]
            );
        }
    );

    abp.ui.extensions.tableColumns.get('masterData.company').addContributor(
        function (columnList) {
            columnList.addManyTail(
                [
                    {
                        title: l("Actions"),
                        rowAction: {
                            items: abp.ui.extensions.entityActions.get('masterData.company').actions.toArray()
                        }
                    },
                    {
                        title: l("Display:CompanyCode"),
                        data: 'code'
                    },
                    {
                        title: l("Display:CompanyName"),
                        data: 'name',
                    },
                    {
                        title: l("Display:CompanyEmailAddress"),
                        data: 'emailAddress',
                    }
                ]
            );
        },
        0 //adds as the first contributor
    );

    $(function () {
        const _$wrapper = $('#CompaniesWrapper');

        _dataTable = _$wrapper.find('table').DataTable(
            abp.libs.datatables.normalizeConfiguration({
                order: [[1, 'asc']],
                processing: true,
                paging: true,
                scrollX: true,
                serverSide: true,
                ajax: abp.libs.datatables.createAjax(_companyAppService.getList),
                columnDefs: abp.ui.extensions.tableColumns.get('masterData.company').columns.toArray(),
            })
        );

        _createModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _editModal.onResult(function () {
            _dataTable.ajax.reload();
        });

        _$wrapper.find('button[name=CreateCompany]').click(function (e) {
            e.preventDefault();
            _createModal.open();
        });
    });
})();
