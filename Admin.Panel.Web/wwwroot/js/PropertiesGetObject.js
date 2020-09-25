$('#ObjectTypeId').change(async event => {
    const id = event.target.value;
    
    const partial = await $.get(`/ObjectsPropValues/GetObjectProperties/${id}`);
    
    $("#option-container").html(partial);
})