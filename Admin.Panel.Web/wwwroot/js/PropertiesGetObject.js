$('#ObjectTypeId').change(async event => {
    const id = event.target.value;
    
    const partial = await $.get(`https://localhost:5001/ObjectsPropValues/GetObjectProperties/${id}`);
    
    $("#option-container").html(partial);
})