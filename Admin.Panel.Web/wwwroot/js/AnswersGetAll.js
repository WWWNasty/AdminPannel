let input = $('.input-answers');

const loadAnswers = async event => {
    const id = event.target.value;

    const index = event.target.attributes.itemIndex.value;

    debugger;

    const partial = await $.get(global.GetUri(`Questionary/AnswersGetAll/${id}?index=${index}`));
    
    const parent = $(event.target).parents('.aa-container');

    const element = parent.find('.input-field-type');

    element.html(partial);

    $(element).find('.new-selectpicker').selectpicker();
};

input.change(loadAnswers); 