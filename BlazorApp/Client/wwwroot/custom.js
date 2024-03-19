// wwwroot/custom.js
window.triggerFileDialog = function () {
    document.querySelector('.hidden-input').click();
};


window.stopEventPropagation = function () {
    event.stopPropagation();
};
