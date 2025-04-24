$(document).ready(function () {
    // Define as máscaras
    const cpfMask = '000.000.000-00';
    const cnpjMask = '00.000.000/0000-00';

    $('#Telefone').mask('(00) 00000-0000'); 

    // Aplica a máscara inicial (CPF)
    $('#Documento').mask(cpfMask);
    $('#ResponsavelEmpresa').mask(cpfMask);
    $("#documentoTxt").text("CPF");

    // Altera a máscara ao marcar/desmarcar o checkbox
    $('#PessoaJuridica').change(function () {
        if ($(this).is(':checked')) {
            $('#Documento').mask(cnpjMask); // Aplica a máscara de CNPJ
            $("#tipoPessoaJuridica").removeClass("hidden");
            $("#btn-consultar-cnpj").removeClass("hidden");
            $("#documentoTxt").text("CNPJ");
        } else {
            $('#Documento').mask(cpfMask); // Aplica a máscara de CPF
            $("#tipoPessoaJuridica").addClass("hidden");
            $("#btn-consultar-cnpj").addClass("hidden");
            $("#documentoTxt").text("CPF");
        }
    });


    document.getElementById("btn-consultar").addEventListener("click", function () {
        const cep = document.getElementById("Cep").value;

        if (!cep) {
            showAlert("Por favor, insira um CEP.", "warning");
            return;
        }

        fetch(`/Cep/ConsultarCep?cep=${cep}`)
            .then(response => {
                if (!response.ok) {
                    showAlert("CEP não encontrado.", "warning");
                }
                return response.json();
            })
            .then(data => {

                
                document.getElementById("Cep").value = data.cep;
                document.getElementById("Uf").value = data.state;
                document.getElementById("Cidade").value = data.city;
                document.getElementById("Bairro").value = data.neighborhood;
                document.getElementById("Endereco").value = data.street;
            })
            .catch(error => {
                alert(error.message);
            });
    });

});