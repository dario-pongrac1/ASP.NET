document.addEventListener('DOMContentLoaded', function () {
    const controls = document.querySelectorAll('[data-datetime-control]');
    if (controls.length === 0) {
        return;
    }

    controls.forEach(function (control) {
        const input = control.querySelector('[data-role="value"]');
        const errorSpan = control.querySelector('[data-valmsg-for]');
        const mode = (control.dataset.mode || 'datetime').toLowerCase();

        if (!input || !errorSpan) {
            return;
        }

        applyBoundaries(control, input, mode);

        input.addEventListener('input', function () {
            clearError(input, errorSpan);
            validateControl(control, input, errorSpan, mode);
        });

        input.addEventListener('change', function () {
            applyBoundaries(control, input, mode);
            validateControl(control, input, errorSpan, mode);
            refreshDependentControls(controls, input.id);
        });

        const group = control.closest('form') || document;
        if (!group.__datetimeControlAttached) {
            group.__datetimeControlAttached = true;
            group.addEventListener('submit', function (event) {
                let isFormValid = true;
                controls.forEach(function (item) {
                    if (group.contains(item)) {
                        const currentInput = item.querySelector('[data-role="value"]');
                        const currentError = item.querySelector('[data-valmsg-for]');
                        const currentMode = (item.dataset.mode || 'datetime').toLowerCase();
                        if (!currentInput || !currentError) {
                            return;
                        }

                        applyBoundaries(item, currentInput, currentMode);
                        const isValid = validateControl(item, currentInput, currentError, currentMode);
                        if (!isValid) {
                            isFormValid = false;
                        }
                    }
                });

                if (!isFormValid) {
                    event.preventDefault();
                }
            });
        }
    });

    function refreshDependentControls(allControls, changedInputId) {
        if (!changedInputId) {
            return;
        }

        allControls.forEach(function (item) {
            const dependentInput = item.querySelector('[data-role="value"]');
            const dependentError = item.querySelector('[data-valmsg-for]');
            const dependentMode = (item.dataset.mode || 'datetime').toLowerCase();
            if (!dependentInput || !dependentError) {
                return;
            }

            const compareTarget = dependentInput.dataset.compareTarget;
            if (compareTarget === '#' + changedInputId) {
                applyBoundaries(item, dependentInput, dependentMode);
                validateControl(item, dependentInput, dependentError, dependentMode);
            }
        });
    }

    function applyBoundaries(control, input, mode) {
        const noPast = input.dataset.noPast === 'true';
        const noFuture = input.dataset.noFuture === 'true';
        const compareTargetSelector = input.dataset.compareTarget;
        const compareMode = (input.dataset.compareMode || 'after').toLowerCase();

        if (noPast) {
            input.min = getNowValue(mode);
        }

        if (noFuture) {
            input.max = getNowValue(mode);
        }

        if (compareTargetSelector && compareMode === 'after') {
            const targetInput = document.querySelector(compareTargetSelector);
            if (targetInput && targetInput.value) {
                input.min = targetInput.value;
            }
        }
    }

    function validateControl(control, input, errorSpan, mode) {
        input.setCustomValidity('');

        const compareTargetSelector = input.dataset.compareTarget;
        const compareMode = (input.dataset.compareMode || 'after').toLowerCase();
        if (compareTargetSelector && compareMode === 'after') {
            const targetInput = document.querySelector(compareTargetSelector);
            if (targetInput && targetInput.value && input.value && input.value <= targetInput.value) {
                input.setCustomValidity('Kraj termina mora biti nakon početka termina.');
            }
        }

        if (!input.checkValidity()) {
            setError(input, errorSpan, resolveMessage(input, mode));
            return false;
        }

        clearError(input, errorSpan);
        return true;
    }

    function resolveMessage(input, mode) {
        if (input.validity.valueMissing) {
            return 'Ovo polje je obavezno.';
        }

        if (input.validity.rangeUnderflow) {
            return mode === 'date'
                ? 'Datum ne može biti u prošlosti.'
                : 'Datum i vrijeme ne mogu biti u prošlosti.';
        }

        if (input.validity.rangeOverflow) {
            return mode === 'date'
                ? 'Datum ne može biti u budućnosti.'
                : 'Datum i vrijeme ne mogu biti u budućnosti.';
        }

        if (input.validity.customError) {
            return input.validationMessage;
        }

        return 'Unesite valjani datum.';
    }

    function getNowValue(mode) {
        const now = new Date();
        const year = String(now.getFullYear());
        const month = String(now.getMonth() + 1).padStart(2, '0');
        const day = String(now.getDate()).padStart(2, '0');

        if (mode === 'date') {
            return `${year}-${month}-${day}`;
        }

        const hour = String(now.getHours()).padStart(2, '0');
        const minute = String(now.getMinutes()).padStart(2, '0');
        return `${year}-${month}-${day}T${hour}:${minute}`;
    }

    function setError(input, errorSpan, message) {
        input.classList.add('is-invalid');
        input.setAttribute('aria-invalid', 'true');
        errorSpan.textContent = message;
    }

    function clearError(input, errorSpan) {
        input.classList.remove('is-invalid');
        input.removeAttribute('aria-invalid');
        errorSpan.textContent = '';
    }
});