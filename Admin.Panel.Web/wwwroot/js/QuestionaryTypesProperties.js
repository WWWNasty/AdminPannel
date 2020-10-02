// Write your JavaScript code.

let countAnsw = 1;
$('.add').click(function () {

    debugger;
    const template = document.getElementById('option-template');
    const newNode = template.cloneNode(true);

    newNode.removeAttribute('style');
    newNode.removeAttribute('id');
    // newNode.dataset.id = count.toString();

    const elements = $(newNode).find('input');
    // const selects = $(newNode).find('.select');
    //
    // //input.id = input.id.replace('0', count.toString()); //name.replace('0', count.toString());
    //
    // selects.each((_, select) => {
    //     select.setAttribute('class', select.class.replace('select', 'selectpicker'));
    // })
    elements.each((_, input) => {
        input.setAttribute('id', input.id.replace('0', countAnsw.toString()));
        input.setAttribute('name', input.name.replace('0', countAnsw.toString()));
    })


    newNode.classList.add ('answer-option');

    // if (count < 30) {
    //     count++;

    let create = $('.add-container').before(newNode);
    countAnsw++;

    // } else {                                                      
    //     alert("Максимум 30 штук");
    // }                                                
})

function addDeleteButtonHandler() {

    $('.delete').click(deleteVotingOption);
}

function deleteVotingOption() {
    debugger;
    if (countAnsw > 1) {
        let del = $('.answer-option').last().remove();
        countAnsw--;
    }
}

$(document).ready(async () => {
    countAnsw = parseInt($('#answer-options-count').text());
    addDeleteButtonHandler();
})