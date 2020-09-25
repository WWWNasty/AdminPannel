$('#class').change(async event => {
    const id = event.target.value;

    const partial = await $.get(`Questionary/_SelectableAnswers/${id}`);

    $("#option-container").html(partial);
})