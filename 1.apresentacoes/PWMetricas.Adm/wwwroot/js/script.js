// js/script.js

document.addEventListener('DOMContentLoaded', function () {
    const toggleBtn = document.querySelector('.toggle-btn');
    const sidebar = document.querySelector('.sidebar');
    const userAvatarTopbar = document.querySelector('.topbar .user-avatar');
    const dropdown = document.querySelector('.dropdown');
    const tabButtons = document.querySelectorAll('.tab-btn');
    const tabContents = document.querySelectorAll('.tab-content');
    const userNameSpanTopbar = document.querySelector('.topbar .user-name');
    const userInfoDiv = document.querySelector('.topbar .user-info');
    const avatarHeader = document.querySelector('.avatar-header');
    const avatarDropdown = document.querySelector('.avatar-dropdown');

    function getRandomAvatarUrl() {
        // Lista de URLs de avatares de alta qualidade (substitua pelas suas)
        const avatars = [
            'https://i.pravatar.cc/150?u=a01',
            'https://i.pravatar.cc/150?u=a02',
            'https://i.pravatar.cc/150?u=a03',
            'https://i.pravatar.cc/150?u=a04',
            'https://i.pravatar.cc/150?u=a05',
            // Adicione mais URLs conforme necessário
        ];
        const randomIndex = Math.floor(Math.random() * avatars.length);
        return avatars[randomIndex];
    }

    // Define um avatar aleatório inicial para ambos os tamanhos
    const initialAvatarUrl = getRandomAvatarUrl();
    if (avatarHeader) {
        avatarHeader.src = initialAvatarUrl;
    }
    if (avatarDropdown) {
        avatarDropdown.src = initialAvatarUrl;
    }

    // Alternar a classe 'expanded' na sidebar e no botão toggle
    toggleBtn.addEventListener('click', () => {
        sidebar.classList.toggle('expanded');
        toggleBtn.classList.toggle('expanded');

        const content = document.querySelector('.content');
        const topbar = document.querySelector('.topbar');
        if (content) content.classList.toggle('sidebar-expanded');
        if (topbar) topbar.classList.toggle('sidebar-expanded');

        const icon = toggleBtn.querySelector('i');
        if (icon.classList.contains('fa-chevron-right')) {
            icon.classList.remove('fa-chevron-right');
            icon.classList.add('fa-chevron-left');
        } else {
            icon.classList.remove('fa-chevron-left');
            icon.classList.add('fa-chevron-right');
        }
    });

    // Alternar a classe 'active' nos botões de aba e mostrar o conteúdo da aba correspondente
    tabButtons.forEach(button => {
        button.addEventListener('click', () => {
            tabButtons.forEach(btn => btn.classList.remove('active'));
            tabContents.forEach(tab => tab.classList.add('hidden'));

            button.classList.add('active');
            const targetTabId = button.dataset.tab;
            if (targetTabId) {
                const targetTab = document.getElementById(targetTabId);
                if (targetTab) {
                    targetTab.classList.remove('hidden');
                }
            }
        });
    });

    // Mostrar a aba ativa inicial
    const activeTabButton = document.querySelector('.tab-btn.active');
    if (activeTabButton) {
        const initialTabId = activeTabButton.dataset.tab;
        if (initialTabId) {
            const initialTab = document.getElementById(initialTabId);
            if (initialTab) {
                initialTab.classList.remove('hidden');
            }
        }
    }

    // Definir nome de usuário no topbar
    const loggedInUserName = 'Nome Do Usuário';
    if (userNameSpanTopbar) {
        userNameSpanTopbar.textContent = loggedInUserName;
    }

    // Alternar dropdown do usuário ao clicar no avatar do topbar
    if (userAvatarTopbar && dropdown && userInfoDiv) {
        userAvatarTopbar.addEventListener('click', () => {
            dropdown.classList.toggle('hidden');
            userInfoDiv.classList.toggle('dropdown-open');
        });

        // Fechar dropdown ao clicar fora
        document.addEventListener('click', (event) => {
            if (!userAvatarTopbar.contains(event.target) && !dropdown.contains(event.target)) {
                dropdown.classList.add('hidden');
                userInfoDiv.classList.remove('dropdown-open');
            }
        });
    }

    function aplicarBandeiras() {
        const tableRows = document.querySelectorAll('#visao-geral tbody tr');
        const countryCodeMap = {
            '+55': 'br',
            '+1': 'us',
            '+54 9 11': 'ar',
            // Adicione mais mapeamentos conforme necessário
        };
        const flagsPath = 'css/flags/'; // Caminho para a pasta das bandeiras

        tableRows.forEach(row => {
            const whatsappCell = row.querySelector('td:nth-child(3)');
            const phoneNumber = whatsappCell.textContent;

            // Remove qualquer imagem de bandeira existente para evitar duplicações
            const existingFlag = whatsappCell.querySelector('.country-flag-img');
            if (existingFlag) {
                whatsappCell.removeChild(existingFlag);
            }

            for (const prefix in countryCodeMap) {
                if (phoneNumber.startsWith(prefix)) {
                    const countryCode = countryCodeMap[prefix];
                    const flagImg = document.createElement('img');
                    flagImg.src = flagsPath + countryCode + '.png';
                    flagImg.alt = getCountryName(countryCode); // Função auxiliar para obter o nome do país (opcional)
                    flagImg.className = 'country-flag-img';
                    whatsappCell.prepend(flagImg); // Adiciona a imagem no início da célula
                    break;
                }
            }
        });
    }

    function getCountryName(code) {
        // Função auxiliar opcional para retornar o nome do país com base no código
        switch (code) {
            case 'br': return 'Brasil';
            case 'us': return 'Estados Unidos';
            case 'ar': return 'Argentina';
            // Adicione mais casos conforme necessário
            default: return '';
        }
    }

    // Chame a função para aplicar as bandeiras quando a página carregar
    aplicarBandeiras();

    // Se a sua tabela for atualizada dinamicamente, chame aplicarBandeiras() novamente.
});