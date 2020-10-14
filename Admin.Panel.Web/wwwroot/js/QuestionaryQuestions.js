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
    let i1 = $(newNode).find('select.input-answers option');
    $.each(i1, function (index , item){
        item.removeAttribute('selected');
        if(index===0){
            item.setAttribute('selected', 'selected');
        }
    });
    
    console.log(i1);
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
    let seq = $(node).find(".sequence-order").last();
    seq.val(i);
    let z = seq.value;
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
    firstSequenceOrder();
    addDeleteButtonHandler();
})

function firstSequenceOrder(){
    debugger;
    const container = $('#simpleList');
    let inputContainer = $(container).find(".aa-container");
    let input = $(inputContainer).find("#init-sequence-order")
    input.val("0");
}

function getInputFieldTypes() {
    let input = $('.input-answers');
    input.change(loadAnswers);
}
