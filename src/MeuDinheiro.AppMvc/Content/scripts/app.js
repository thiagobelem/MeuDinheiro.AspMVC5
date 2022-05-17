
function carregarLista() {
    var tabela = $("#listaTable");
    $(tabela).load(tabela.data("url"));
}

function validarForm() {
    $.validator.unobtrusive.parse('#Form');
    $("#btnSubmit").click(function () {
        if (!$("#Form").valid()) {
            return false;
        }
    });
}

$("#btnCadastrar").on("click", function (e) {
    $("#modal-content").load(this.href,
        function () {
            $("#Modal").modal("show");
        });
    return false;
});

$("#listaTable").on('click', ".btnDetalhes", function (e) {
    $(this).addClass("disabled");
    $("#modal-content").load($(this).data("url"), function (responseTxt, statusTxt, xhr) {
        if (statusTxt == "success") {
            $("#Modal").modal("show");
        }
        if (statusTxt == "error") {
            swal({
                title: xhr.status,
                text: xhr.statusText,
                type: "error",
                confirmButtonColor: "#f47920",
                allowEscapeKey: "true"
            });
        }
    });
});

$("#listaTable").on('click', ".btnEditar", function (e) {
    $(this).addClass("disabled");
    $("#modal-content").load($(this).data("url"), function (responseTxt, statusTxt, xhr) {
        if (statusTxt == "success") {
            $("#Modal").modal("show");
        }
        if (statusTxt == "error") {
            swal({
                title: xhr.status,
                text: xhr.statusText,
                type: "error",
                confirmButtonColor: "#f47920",
                allowEscapeKey: "true"
            });
        }
    });
});

$("#listaTable").on('click', ".btnExcluir", function () {
    var ID = $(this).data("value");
    var URL = $(this).data("url");
    var TITLE = $(this).data("title");
    var TEXT = $(this).data("text");
    var TOKEN = $("#token").val();
    swal({
        title: TITLE,
        text: TEXT,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#fb6340",
        confirmButtonText: "Sim",
        cancelButtonText: "Não",
        cancelButtonColor: "#f4f5f7",
        animation: "slide-from-top",
        closeOnConfirm: false
    },
        function () {
            $.post(
                {
                    url: URL,
                    data: {
                        id: ID, __RequestVerificationToken: TOKEN
        },
        success: function (data) {
            if (data.status === "success") {
                carregarLista();
                swal({
                    title: "Feito!",
                    text:  data.message,
                    type: "success",
                    confirmButtonColor: "#00995d",
                    allowEscapeKey: "true"
                });
            }
            else {
                if (data.status === "failure") {
                    var listerrors = "";
                    if (Array.isArray(data.formErrors)) {
                        data.formErrors.forEach(function (item) {
                            item.errors.forEach(function (item2) {
                                listerrors += "* " + item2 + "\n";
                            })
                        })
                    }
                    else {
                        listerrors = data.formErrors;
                    }
                    swal({
                        title: "Erro !",
                        text: listerrors,
                        type: "error",
                        confirmButtonColor: "#F47920",
                        allowEscapeKey: "false"
                    });
                }
            }
        },
        error: function (error) {
            console.log(error);
            alert(error)
        }
        });
     });
});

function onChangeSucess(data) {
    if (data.status === "success") {
        $("#Modal").modal('hide');
        carregarLista();
        swal({
            title: "Feito!",
            text: data.message,
            type: "success",
            confirmButtonColor: "#0A5F55",
            allowEscapeKey: "true",
            closeOnConfirm: true
        });
    }
    else {
        if (data.status === "failure") {
            var listerrors = "";
            if (Array.isArray(data.formErrors)) {
                data.formErrors.forEach(function (item) {
                    item.errors.forEach(function (item2) {
                        listerrors += "* " + item2 + "\n";
                    })
                })
            }
            else {
                listerrors = data.formErrors;
            }
            swal({
                title: "Erro !",
                text: listerrors,
                type: "error",
                confirmButtonColor: "#F47920",
                allowEscapeKey: "false"
            });
            $("#btnSubmit").removeAttr("disabled", "disabled");
        }
    }
}

function onChangeFailure(data) {
    var listerrors = "";
    $(data.formErrors).each(function (index, value) {
        listerrors += value.errors + "\n";
    });
    listerrors = listerrors.replace(",", "\n");
    swal({
        title: "Erro !",
        text: listerrors,
        type: "error",
        confirmButtonColor: "#F47920",
        allowEscapeKey: "false"
    });
}

$("#Modal").on('hide.bs.modal', function () {
    ativarBotoes();
});

function ativarBotoes() {
    $("a").removeClass("disabled");
}