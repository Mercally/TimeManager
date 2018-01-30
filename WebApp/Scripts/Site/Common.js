$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

$('body').on('click', 'a.btnDelete', function (evt) {
    evt.preventDefault();
    var $this = $(this);
    if (confirm('¿Seguro que desea eliminar este elemento?')) {
        $.post($this.attr('href'), function (response) {
            if (response.IsSuccess) {
                window.location.reload();
            } else {
                alert('Ocurrió un error al eliminar el elemento!');
            }
        });
    }
});