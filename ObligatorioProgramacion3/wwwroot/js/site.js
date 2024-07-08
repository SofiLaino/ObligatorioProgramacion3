document.addEventListener('DOMContentLoaded', function () {
    // Selecciona todos los elementos con la clase 'custom-select'
    var selects = document.querySelectorAll('.custom-select');

    // Itera sobre cada elemento select encontrado
    selects.forEach(function (select) {
        // Crea un nuevo div para envolver el select
        var wrapper = document.createElement('div');
        wrapper.className = 'custom-select-wrapper'; // Asigna la clase 'custom-select-wrapper'

        // Inserta el wrapper antes del select en el DOM
        select.parentNode.insertBefore(wrapper, select);

        // Mueve el select dentro del wrapper
        wrapper.appendChild(select);

        // Crea un nuevo div para la flecha del select
        var arrow = document.createElement('div');
        arrow.className = 'custom-select-arrow'; // Asigna la clase 'custom-select-arrow'
        arrow.innerHTML = '▼'; // Añade el símbolo de la flecha

        // Añade la flecha al wrapper
        wrapper.appendChild(arrow);
    });
});
