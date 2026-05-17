/**
 * Autocomplete Dropdown JavaScript
 * Koristi AJAX za učitavanje rezultata s debounce-om
 */

document.addEventListener('DOMContentLoaded', function () {
    const autocompleteInputs = document.querySelectorAll('.autocomplete-input');

    autocompleteInputs.forEach(input => {
        const endpoint = input.dataset.endpoint;
        const hiddenInput = document.querySelector(input.dataset.hiddenInput);
        const resultsContainer = input.parentElement.querySelector('.autocomplete-results');
        const resultsList = resultsContainer.querySelector('ul');

        let debounceTimer;

        // Input event sa debounce-om
        input.addEventListener('input', function () {
            clearTimeout(debounceTimer);

            const query = this.value.trim();

            // Ako je unos manji od 2 karaktera ili je prazan, sakrij rezultate
            if (query.length < 2) {
                resultsContainer.style.display = 'none';
                resultsList.innerHTML = '';
                return;
            }

            // Debounce AJAX poziv (300ms)
            debounceTimer = setTimeout(() => {
                fetchAutocompleteResults(endpoint, query, resultsList, resultsContainer, hiddenInput, input);
            }, 300);
        });

        // Klik izvan dropdown-a = zatvaranje
        document.addEventListener('click', function (e) {
            if (!input.contains(e.target) && !resultsContainer.contains(e.target)) {
                resultsContainer.style.display = 'none';
            }
        });

        // Enter ili Arrow down = otvori rezultate ako su dostupni
        input.addEventListener('keydown', function (e) {
            if ((e.key === 'Enter' || e.key === 'ArrowDown') && resultsContainer.style.display === 'block') {
                const firstItem = resultsList.querySelector('li');
                if (firstItem) {
                    firstItem.focus();
                }
            }
        });
    });

    /**
     * Fetch autocomplete rezultate sa servera
     */
    function fetchAutocompleteResults(endpoint, query, resultsList, resultsContainer, hiddenInput, input) {
        input.classList.add('autocomplete-loading');

        fetch(`${endpoint}?q=${encodeURIComponent(query)}`)
            .then(response => {
                if (!response.ok) throw new Error('Network response was not ok');
                return response.json();
            })
            .then(data => {
                input.classList.remove('autocomplete-loading');

                resultsList.innerHTML = '';

                if (data.length === 0) {
                    const li = document.createElement('li');
                    li.className = 'list-group-item disabled';
                    li.textContent = 'Nema rezultata';
                    resultsList.appendChild(li);
                    resultsContainer.style.display = 'block';
                    return;
                }

                data.forEach(item => {
                    const li = document.createElement('li');
                    li.className = 'list-group-item';
                    li.textContent = item.label;
                    li.dataset.value = item.value;
                    li.tabIndex = 0;

                    li.addEventListener('click', function () {
                        selectAutocompleteItem(item, hiddenInput, input, resultsContainer);
                    });

                    li.addEventListener('keydown', function (e) {
                        if (e.key === 'Enter') {
                            selectAutocompleteItem(item, hiddenInput, input, resultsContainer);
                        }
                    });

                    resultsList.appendChild(li);
                });

                resultsContainer.style.display = 'block';
            })
            .catch(error => {
                input.classList.remove('autocomplete-loading');
                console.error('Autocomplete error:', error);
                const li = document.createElement('li');
                li.className = 'list-group-item disabled text-danger';
                li.textContent = 'Greška pri učitavanju rezultata';
                resultsList.innerHTML = '';
                resultsList.appendChild(li);
                resultsContainer.style.display = 'block';
            });
    }

    /**
     * Odabir stavke iz autocomplete liste
     */
    function selectAutocompleteItem(item, hiddenInput, input, resultsContainer) {
        hiddenInput.value = item.value;
        input.value = item.label;
        resultsContainer.style.display = 'none';
    }
});
