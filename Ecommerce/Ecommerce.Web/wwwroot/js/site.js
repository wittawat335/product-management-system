$(document).ready(function () {
    $.fn.serializeObjectTable = function () {
        var o = {};

        var table = this.DataTable();
        var a = table.$('input, select').serialize();
        var parms = {};
        var items = a.split("&");
        for (var i = 0; i < items.length; i++) {
            var values = items[i].split("=");
            var key = decodeURIComponent(values.shift());
            var value = values.join("=")
            parms[key] = decodeURIComponent(value);
        }
        return (parms);
        return o;
    };
    $.fn.serializeObject = function () {
        var o = {};
        var disabledInput = this.find(':input:disabled').removeAttr('disabled')
        var a = this.find('input[name],select[name],textarea[name]').not('input[type=hidden]').serializeArray();

        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        disabledInput.attr('disabled', 'disabled');
        return o;
    };
});
function formSave(formId, url) {
    var data = $('#' + formId).serializeObject();
    $.post(url, data, function (response) {
        console.log(response);
        if (response.isSuccess) {
            swalMessage('success', response.message);
            closeModal();
            GetList();
        }
        else swalMessage('error', response.message);
    });
}
function modalPOST(caption, path, data, isFull) {
    var url = path;
    $.post(url, data, function (result) {
        $('#modalDialog > .modal-dialog > .modal-content > .modal-body').html(result);
        showModal(caption, isFull);
    });
}
function modalPOSTV2(caption, path, data, isFull) {
    var url = path;
    $.post(url, data, function (result) {
        $('#modalDialogLv2 > .modal-dialog > .modal-content > .modal-body').html(result);
        showModalLv2(caption, isFull);
    });
}
function showModal(caption, isFull, med) {

    $('#modalDialog > .modal-dialog').removeClass('modal-full');
    $('#modalDialog > .modal-dialog').removeClass('modal-20');
    $('#modalDialog > .modal-dialog').removeClass('modal-30');
    $('#modalDialog > .modal-dialog').removeClass('modal-40');
    $('#modalDialog > .modal-dialog').removeClass('modal-50');
    $('#modalDialog > .modal-dialog').removeClass('modal-55');
    $('#modalDialog > .modal-dialog').removeClass('modal-60');
    $('#modalDialog > .modal-dialog').removeClass('modal-60');
    $('#modalDialog > .modal-dialog').removeClass('modal-70');
    $('#modalDialog > .modal-dialog').removeClass('modal-75');
    $('#modalDialog > .modal-dialog').removeClass('modal-80');
    $('#modalDialog > .modal-dialog').removeClass('modal-90');
    $('#modalDialog > .modal-dialog').removeClass('modal-99');

    if (typeof (isFull) === "boolean") {
        if (isFull)
            $('#modalDialog > .modal-dialog').addClass('modal-full');
        else
            $('#modalDialog > .modal-dialog').removeClass('modal-full');
    } else {
        if (typeof (isFull) === "number") {
            var x = isFull;
            switch (true) {
                case (x >= 20 && x < 30):
                    $('#modalDialog > .modal-dialog').addClass('modal-20');
                    break;
                case (x >= 30 && x < 38):
                    $('#modalDialog > .modal-dialog').addClass('modal-36');
                    break;
                case (x >= 30 && x < 40):
                    $('#modalDialog > .modal-dialog').addClass('modal-30');
                    break;
                case (x >= 40 && x < 50):
                    $('#modalDialog > .modal-dialog').addClass('modal-40');
                    break;
                case (x >= 50 && x < 60):
                    $('#modalDialog > .modal-dialog').addClass('modal-50');
                    break;
                case (x >= 55 && x < 60):
                    $('#modalDialog > .modal-dialog').addClass('modal-55');
                    break;
                case (x >= 60 && x < 70):
                    $('#modalDialog > .modal-dialog').addClass('modal-60');
                    break;
                case (x >= 70 && x < 80):
                    $('#modalDialog > .modal-dialog').addClass('modal-70');
                    break;
                case (x >= 70 && x < 76):
                    $('#modalDialog > .modal-dialog').addClass('modal-75');
                    break;
                case (x >= 80 && x < 90):
                    $('#modalDialog > .modal-dialog').addClass('modal-80');
                    break;
                case (x >= 90 && x < 95):
                    $('#modalDialog > .modal-dialog').addClass('modal-90');
                    break;
                case (x >= 95):
                    $('#modalDialog > .modal-dialog').addClass('modal-99');
                    break;
                default:
                    $('#modalDialog > .modal-dialog').addClass('modal-full');
                    break;
            }
        }
    }

    $('#modalDialog > .modal-dialog > .modal-content > .modal-header > .modal-title').text(caption);
    $('#modalDialog').modal('show');
}
function showModalLv2(caption, isFull) {
    if (typeof (isFull) === "boolean") {
        if (isFull)
            $('#modalDialogLv2 > .modal-dialog').addClass('modal-full');
        else
            $('#modalDialogLv2 > .modal-dialog').removeClass('modal-full');
    } else {
        if (typeof (isFull) === "number") {
            var x = isFull;
            switch (true) {
                case (x >= 20 && x < 30):
                    $('#modalDialog > .modal-dialog').addClass('modal-20');
                    break;
                case (x >= 30 && x < 40):
                    $('#modalDialog > .modal-dialog').addClass('modal-30');
                    break;
                case (x >= 40 && x < 50):
                    $('#modalDialog > .modal-dialog').addClass('modal-40');
                    break;
                case (x >= 50 && x < 60):
                    $('#modalDialog > .modal-dialog').addClass('modal-50');
                    break;
                case (x >= 60 && x < 70):
                    $('#modalDialog > .modal-dialog').addClass('modal-60');
                    break;
                case (x >= 70 && x < 80):
                    $('#modalDialog > .modal-dialog').addClass('modal-70');
                    break;
                case (x >= 80 && x < 90):
                    $('#modalDialog > .modal-dialog').addClass('modal-80');
                    break;
                case (x >= 90):
                    $('#modalDialog > .modal-dialog').addClass('modal-90');
                    break;
                default:
                    $('#modalDialog > .modal-dialog').addClass('modal-full');
                    break;
            }
        }
    }
    $('#modalDialogLv2 > .modal-dialog > .modal-content > .modal-header > .modal-title').text(caption);
    $('#modalDialogLv2').modal('show');
}
function closeModal() {
    $('#modalDialog > .modal-dialog > .modal-content > .modal-body').html('');
    $('#modalDialog > .modal-dialog > .modal-content > .modal-header > .modal-title').text('');
    $('#modalDialog').modal('hide');
}
function clearModalLv2() {
    $('#modalDialogLv2 > .modal-dialog > .modal-content > .modal-body').html('');
    $('#modalDialogLv2 > .modal-dialog > .modal-content > .modal-header > .modal-title').text('');
    $('#modalDialogLv2').modal('hide');
}
function ClearValueByDiv(div) {
    $('#' + div + ' input').val("");
    $('#' + div + ' select').val("");
    $('#' + div + ' textarea').val(" ");
}
function SwalProgressBar(icon, message) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: icon,
        title: message
    })
}
function swalMessage(icon, message) {
    Swal.fire({
        icon: icon,
        title: message,
        showConfirmButton: false,
        timer: 1000
    });
}
function confirmMessage() {
    Swal.fire({
        title: "System error - Do you want error message?",
        icon: 'info',
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Yes',
        showCancelButton: true,
        cancelButtonColor: '#d33',
        cancelButtonText: 'No',
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "../Error/Index";
        }
    });
}
function confirmDelete(code, name, url) {
    Swal.fire({
        title: 'Do you want to delete' + ' ' + ' " ' + name + ' " ' + ' ' + '?',
        icon: 'warning',
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Yes',
        showCancelButton: true,
        cancelButtonColor: '#d33',
        cancelButtonText: 'No',
    }).then((result) => {
        if (result.isConfirmed) {
            Delete(code, url);
        }
    });
}
function Delete(code, url) {
    var url = url;
    var data = { "code": code };

    $.post(url, data, function (result) {
        if (result.status) {
            swalMessage('success', result.message);
            closeModal();
            GetList();
        }
        else {
            swalMessage('error', result.message);
        }
    });
}
function SetReadOnlyByDiv(div, x) {
    if (x) {
        $('#' + div).find('input[name],select[name],textarea[name]').not('input[type=hidden]').removeAttr('disabled').removeAttr('readonly')
            .attr('disabled', 'disabled').attr('readonly', 'readonly');
    }
    else {
        $('#' + div).find('input[name],select[name],textarea[name]').not('input[type=hidden]').removeAttr('disabled').removeAttr('readonly');
    }
}
function SetReadOnly(div, x) {
    if (!x) {
        $('#' + div).removeAttr('disabled').removeAttr('readonly')
            .attr('disabled', 'disabled').attr('readonly', 'readonly');
    }
    else {
        $('#' + div).removeAttr('disabled').removeAttr('readonly');
    }
}
function SetReqByDiv(div, x) {
    if (x) {
        $('#' + div + ' input,select').removeAttr('required').attr('required', 'required');
    }
    else {
        $('#' + div + ' input,select').removeAttr('required');
    }
}
function SetReq(div, x) {
    if (x) {
        $('#' + div).removeAttr('required').attr('required', 'required');
    }
    else {

        $('#' + div).removeAttr('required');
        $('#' + div).closest('div.form-group').removeClass('has-error has-danger');
    }
}

