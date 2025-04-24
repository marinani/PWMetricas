function showAlert(message, type = 'success') {
    const alertId = `alert-${Date.now()}`;
    const alertHtml = `
        <div id="${alertId}" class="alert alert-${type} alert-dismissible fade show" role="alert" style="min-width: 300px;">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

    // Adiciona o alerta ao contêiner
    const alertContainer = document.getElementById('alert-container');
    if (alertContainer) {
        alertContainer.insertAdjacentHTML('beforeend', alertHtml);

        // Remove o alerta automaticamente após 4 segundos
        setTimeout(() => {
            const alertElement = document.getElementById(alertId);
            if (alertElement) {
                alertElement.remove();
            }
        }, 4000);
    }
}