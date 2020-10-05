// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let count = 1;
$('.addPick').click(function () {

    const template = document.getElementById('voting-option-template');
    const newNode = template.cloneNode(true);

    newNode.removeAttribute('style');
    newNode.removeAttribute('id');
    newNode.dataset.id = count.toString();

    const elements = $(newNode).find('input, select');
    // const selects = $(newNode).find('.select');
    //
    // //input.id = input.id.replace('0', count.toString()); //name.replace('0', count.toString());
    //
    // selects.each((_, select) => {
    //     select.setAttribute('class', select.class.replace('select', 'selectpicker'));
    // })
    elements.each((_, input) => {
        input.setAttribute('id', input.id.replace('0', count.toString()));
        input.setAttribute('itemIndex', count.toString());
        input.setAttribute('name', input.name.replace('0', count.toString()));
    })


    newNode.classList.add ('voting-option');
    // newNode.classList.add ('js-remove');

    // if (count < 30) {
    //     count++;
    debugger;

    //откуда берем data-id
    let votingOptionSets = $('#simpleList').find(".voting-option-set").toArray();
    //куда записываем индекс
    let inputs = $('#simpleList').find(".sequence-order").toArray();
    let index = 0;
    let i = 0;
    for(index; index < votingOptionSets.length; index++){
        inputs[index].value = index;
        console.log(index, "сортировка записана в модель");
        i = index+1;
    }
    let node = $('#simpleList').append(newNode);
    $(node).find(".voting-option-set").value = i;
    getInputFieldTypes();
    count++;

    $(newNode).find('.new-selectpicker').selectpicker();
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

function getInputFieldTypes() {

    let input = $('.input-answers');

    input.change(async event => {
        const id = event.target.value;

        const index = event.target.attributes.itemIndex.value;

        debugger;

        const partial = await $.get(`/Questionary/AnswersGetAll/${id}?index=${index}`);

        const optionContainer = $(event.target.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement).find('.option-container');

        optionContainer.html(partial);
        
        $(optionContainer).find('.new-selectpicker').selectpicker();
    })
}
