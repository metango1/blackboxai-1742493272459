// Common JavaScript functions for H82Travels

// Form validation
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form.checkValidity()) {
        event.preventDefault();
        event.stopPropagation();
    }
    form.classList.add('was-validated');
}

// Dynamic loading indicator
function showLoading(buttonId) {
    const button = document.getElementById(buttonId);
    const originalText = button.innerHTML;
    button.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...';
    button.disabled = true;
    return originalText;
}

function hideLoading(buttonId, originalText) {
    const button = document.getElementById(buttonId);
    button.innerHTML = originalText;
    button.disabled = false;
}

// Table search functionality
function searchTable(inputId, tableId) {
    const input = document.getElementById(inputId);
    const filter = input.value.toLowerCase();
    const table = document.getElementById(tableId);
    const rows = table.getElementsByTagName('tr');

    for (let i = 1; i < rows.length; i++) {
        let found = false;
        const cells = rows[i].getElementsByTagName('td');
        
        for (let j = 0; j < cells.length; j++) {
            const cell = cells[j];
            if (cell) {
                const text = cell.textContent || cell.innerText;
                if (text.toLowerCase().indexOf(filter) > -1) {
                    found = true;
                    break;
                }
            }
        }
        
        rows[i].style.display = found ? '' : 'none';
    }
}

// Date range validation
function validateDateRange(startDateId, endDateId) {
    const startDate = document.getElementById(startDateId).value;
    const endDate = document.getElementById(endDateId).value;
    
    if (startDate && endDate && new Date(startDate) > new Date(endDate)) {
        alert('End date must be after start date');
        return false;
    }
    return true;
}

// Confirmation dialog
function confirmAction(message) {
    return confirm(message);
}

// Format currency
function formatCurrency(amount) {
    return new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
    }).format(amount);
}

// Format date
function formatDate(dateString) {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    return new Date(dateString).toLocaleDateString(undefined, options);
}

// Handle AJAX errors
function handleAjaxError(xhr, status, error) {
    console.error('AJAX Error:', status, error);
    alert('An error occurred. Please try again later.');
}

// Initialize tooltips
document.addEventListener('DOMContentLoaded', function() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
});

// Initialize popovers
document.addEventListener('DOMContentLoaded', function() {
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    var popoverList = popoverTriggerList.map(function(popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
});

// Handle form submission with AJAX
function submitFormAjax(formId, successCallback) {
    const form = document.getElementById(formId);
    const formData = new FormData(form);

    fetch(form.action, {
        method: form.method,
        body: formData
    })
    .then(response => response.json())
    .then(data => {
        if (successCallback) {
            successCallback(data);
        }
    })
    .catch(error => {
        console.error('Error:', error);
        alert('An error occurred while submitting the form.');
    });
}

// Dynamic form fields
function addFormField(containerId, template) {
    const container = document.getElementById(containerId);
    const newField = template.cloneNode(true);
    container.appendChild(newField);
}

function removeFormField(button) {
    button.closest('.form-group').remove();
}

// Notification system
function showNotification(message, type = 'info') {
    const notification = document.createElement('div');
    notification.className = `alert alert-${type} alert-dismissible fade show`;
    notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    `;
    
    document.getElementById('notification-container').appendChild(notification);
    
    setTimeout(() => {
        notification.remove();
    }, 5000);
}