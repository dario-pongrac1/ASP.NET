document.addEventListener('DOMContentLoaded', function () {
    const controls = document.querySelectorAll('[data-datetime-control]');
    if (controls.length === 0) {
        return;
    }

    controls.forEach(function (control) {
        const displayInput = control.querySelector('[data-role="display"]');
        const hiddenInput = control.querySelector('[data-role="value"]');
        const errorSpan = control.querySelector('[data-valmsg-for]');
        const mode = (control.dataset.mode || 'datetime').toLowerCase();
        const locale = (displayInput.dataset.locale || navigator.language || 'hr-HR').toLowerCase();
        const resolvedLocale = locale.startsWith('hr') ? 'hr-HR' : 'en-US';

        if (!displayInput || !hiddenInput || !errorSpan) {
            return;
        }

        const group = control.closest('form') || document;
        if (!group.__datetimeControlAttached) {
            group.__datetimeControlAttached = true;
            group.addEventListener('submit', function () {
                controls.forEach(function (item) {
                    if (group.contains(item)) {
                        syncControl(item, true);
                    }
                });
            });
        }

        initializeFromHidden(displayInput, hiddenInput, mode, resolvedLocale);

        displayInput.addEventListener('blur', function () {
            syncControl(control, true);
        });

        displayInput.addEventListener('input', function () {
            clearError(displayInput, errorSpan);
        });

        hiddenInput.addEventListener('change', function () {
            initializeFromHidden(displayInput, hiddenInput, mode, resolvedLocale);
        });

        function syncControl(currentControl, validateCompare) {
            const currentDisplay = currentControl.querySelector('[data-role="display"]');
            const currentHidden = currentControl.querySelector('[data-role="value"]');
            const currentError = currentControl.querySelector('[data-valmsg-for]');
            const currentMode = (currentControl.dataset.mode || 'datetime').toLowerCase();

            const parsed = parseDisplayValue(currentDisplay.value, currentMode, resolvedLocale);
            if (!parsed) {
                currentHidden.value = '';
                setError(currentDisplay, currentError, currentDisplay.value.trim().length > 0 ? 'Unesite valjani datum.' : 'Ovo polje je obavezno.');
                return false;
            }

            if (!validateDateRules(currentControl, parsed, currentMode, currentDisplay, currentError, validateCompare)) {
                currentHidden.value = '';
                return false;
            }

            currentHidden.value = toStorageValue(parsed, currentMode);
            clearError(currentDisplay, currentError);
            return true;
        }
    });

    function initializeFromHidden(displayInput, hiddenInput, mode, locale) {
        const hiddenValue = hiddenInput.value;
        if (!hiddenValue) {
            return;
        }

        const parsed = parseStorageValue(hiddenValue);
        if (parsed) {
            displayInput.value = formatForDisplay(parsed, mode, locale);
        }
    }

    function parseStorageValue(value) {
        const parsed = new Date(value);
        return Number.isNaN(parsed.getTime()) ? null : parsed;
    }

    function parseDisplayValue(value, mode, locale) {
        const trimmed = value.trim();
        if (!trimmed) {
            return null;
        }

        const isoMatch = trimmed.match(/^(\d{4})-(\d{2})-(\d{2})(?:T(\d{2}):(\d{2}))?$/);
        if (isoMatch) {
            const year = Number(isoMatch[1]);
            const month = Number(isoMatch[2]);
            const day = Number(isoMatch[3]);
            const hour = Number(isoMatch[4] || 0);
            const minute = Number(isoMatch[5] || 0);
            return buildDate(year, month, day, hour, minute);
        }

        if (locale.startsWith('hr')) {
            if (mode === 'date') {
                const match = trimmed.match(/^(\d{1,2})\.(\d{1,2})\.(\d{4})$/);
                return match ? buildDate(Number(match[3]), Number(match[2]), Number(match[1]), 0, 0) : null;
            }

            const match = trimmed.match(/^(\d{1,2})\.(\d{1,2})\.(\d{4})(?:\s+(\d{1,2}):(\d{2}))$/);
            return match ? buildDate(Number(match[3]), Number(match[2]), Number(match[1]), Number(match[4]), Number(match[5])) : null;
        }

        if (locale.startsWith('en')) {
            if (mode === 'date') {
                const match = trimmed.match(/^(\d{1,2})\/(\d{1,2})\/(\d{4})$/);
                return match ? buildDate(Number(match[3]), Number(match[1]), Number(match[2]), 0, 0) : null;
            }

            const match = trimmed.match(/^(\d{1,2})\/(\d{1,2})\/(\d{4})(?:\s+(\d{1,2}):(\d{2})(?:\s*(AM|PM))?)$/i);
            if (!match) {
                return null;
            }

            let hour = Number(match[4]);
            const minute = Number(match[5]);
            const meridiem = (match[6] || '').toUpperCase();
            if (meridiem === 'PM' && hour < 12) {
                hour += 12;
            }
            if (meridiem === 'AM' && hour === 12) {
                hour = 0;
            }

            return buildDate(Number(match[3]), Number(match[1]), Number(match[2]), hour, minute);
        }

        return null;
    }

    function buildDate(year, month, day, hour, minute) {
        const candidate = new Date(year, month - 1, day, hour, minute, 0, 0);
        return Number.isNaN(candidate.getTime()) ? null : candidate;
    }

    function toStorageValue(date, mode) {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');

        if (mode === 'date') {
            return `${year}-${month}-${day}`;
        }

        const hour = String(date.getHours()).padStart(2, '0');
        const minute = String(date.getMinutes()).padStart(2, '0');
        return `${year}-${month}-${day}T${hour}:${minute}`;
    }

    function formatForDisplay(date, mode, locale) {
        if (mode === 'date') {
            return new Intl.DateTimeFormat(locale, {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            }).format(date);
        }

        return new Intl.DateTimeFormat(locale, {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        }).format(date);
    }

    function validateDateRules(control, date, mode, displayInput, errorSpan, validateCompare) {
        const noPast = control.dataset.noPast === 'true';
        const noFuture = control.dataset.noFuture === 'true';
        const compareTargetSelector = control.dataset.compareTarget;
        const compareMode = (control.dataset.compareMode || 'after').toLowerCase();

        if (noPast) {
            const now = new Date();
            now.setSeconds(0, 0);
            if (mode === 'date') {
                const today = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0, 0);
                if (date < today) {
                    setError(displayInput, errorSpan, 'Datum ne može biti u prošlosti.');
                    return false;
                }
            } else if (date < now) {
                setError(displayInput, errorSpan, 'Datum i vrijeme ne mogu biti u prošlosti.');
                return false;
            }
        }

        if (noFuture) {
            const now = new Date();
            if (mode === 'date') {
                const today = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 0, 0, 0, 0);
                if (date > today) {
                    setError(displayInput, errorSpan, 'Datum ne može biti u budućnosti.');
                    return false;
                }
            } else {
                now.setSeconds(59, 999);
                if (date > now) {
                    setError(displayInput, errorSpan, 'Datum i vrijeme ne mogu biti u budućnosti.');
                    return false;
                }
            }
        }

        if (validateCompare && compareTargetSelector) {
            const compareTarget = document.querySelector(compareTargetSelector);
            if (compareTarget) {
                const compareControl = compareTarget.closest('[data-datetime-control]');
                if (compareControl) {
                    const compareHidden = compareControl.querySelector('[data-role="value"]');
                    const compareValue = parseStorageValue(compareHidden.value);
                    if (compareValue) {
                        const compareDate = compareValue;
                        if (compareMode === 'after' && date <= compareDate) {
                            setError(displayInput, errorSpan, 'Kraj termina mora biti nakon početka termina.');
                            return false;
                        }
                    }
                }
            }
        }

        return true;
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