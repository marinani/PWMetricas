document.addEventListener("DOMContentLoaded", function () {
    const listagemContainer = document.getElementById("loja-listagem");

    if (listagemContainer) {
        listagemContainer.addEventListener("click", function (event) {
            const target = event.target;

            if (target.classList.contains("page-link")) {
                event.preventDefault();

                const page = target.getAttribute("data-page");

                // Faz a requisição AJAX para atualizar a listagem
                fetch(`/Loja/Consulta?page=${page}`, {
                    method: "GET",
                    headers: {
                        "X-Requested-With": "XMLHttpRequest"
                    }
                })
                    .then(response => {
                        if (!response.ok) {
                            showAlert("Erro ao carregar a página.", "error");
                            return;
                        }
                        return response.text();
                    })
                    .then(html => {
                        // Atualiza o conteúdo da listagem
                        listagemContainer.innerHTML = html;
                    })
                    .catch(error => {
                        console.error("Erro:", error);
                    });
            }
        });
    }
});
