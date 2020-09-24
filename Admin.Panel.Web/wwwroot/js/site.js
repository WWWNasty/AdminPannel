// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let count = 1;
$('.addPick').click(function () {
 
    const template = document.getElementById('voting-option-template');
    const newNode = template.cloneNode(true);

    newNode.removeAttribute('style');
    newNode.removeAttribute('id');

    const elements = $(newNode).find('input');

    //input.id = input.id.replace('0', count.toString()); //name.replace('0', count.toString());
    
    elements.each((_, input) => {
        input.setAttribute('id', input.id.replace('0', count.toString()));
        input.setAttribute('name', input.name.replace('0', count.toString()));
    })

    newNode.classList.add ('voting-option');
    
    if (count < 30) {
        count++;

        $('.add-option-container').before(newNode);
        
    } else {                                                      
        alert("Максимум 30 штук");
    }                                                
})

function addDeleteButtonHandler() {

    $('.deletePick').click(deleteVotingOption);
}

function deleteVotingOption() {
    if (count > 1) {
        $('.voting-option').last().remove();
        count--;
    }
}

$(document).ready(async () => {
    count = parseInt($('#voting-options-count').text());

    addDeleteButtonHandler();
})