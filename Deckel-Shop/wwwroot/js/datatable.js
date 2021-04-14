$(document).ready(function () {
   
    var table = $('#myTable').DataTable({
        
        language: {
            searchPanes: {
                clearMessage: 'Clear all filter',
                collapse: { 0: 'Advanced Search Options', _: 'Search Options (%d)' }
            }
        },

        buttons: [
            {
                extend: 'searchPanes',
                config: {
                    cascadePanes: true
                }
            }
        ],
        dom: 'Bfrtip'
    });

});

