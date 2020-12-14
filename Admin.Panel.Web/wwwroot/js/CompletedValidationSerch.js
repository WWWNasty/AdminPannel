$(document).ready(async () => {
    // debugger;
    // let selects = $('.selectpicker');
    // if (selects.get(0).value !== "" || selects.get(1).value !== "" || selects.get(2).value !== ""){
    //     selects.each((_, select) => {
    //         if (select.value === "") {
    //             const parent = $(select).parents('.bootstrap-select');
    //             let dropdown = $(parent).find('.dropdown-toggle');
    //             dropdown.prop('disabled', true);
    //         }
    //     })
    // }
})

function replaceOptions(selector, options) {
    const select = $(selector);
    
    select.empty();
    
    options.forEach(option => select.append(`<option value="${option.Id}">${option.Name}</option>`));

    select.selectpicker('refresh');
}

let companiesFilter = $('.companiesFilter');
const GetSearchObjects = async event => {
    const values = $(event.target).selectpicker('val');
    console.log(values);
    const filteredTypesObj = formData.objectTypes.filter(type => values.includes(type.CompanyId.toString()));
    replaceOptions('#ObjectTypeIds', filteredTypesObj);
    debugger;
    let objTypeIds = filteredTypesObj.map(type => type.Id);
   
    const filteredObj = formData.objects.filter(obj => objTypeIds.includes(obj.ObjectTypeId));
    replaceOptions('#ObjectIds', filteredObj);
};

companiesFilter.change(GetSearchObjects);

let objecTypesFilter = $('.objecTypesFilter');
const GetObjects = async event => {
    const values = $(event.target).selectpicker('val');
    console.log(values);
    const filteredObj = formData.objects.filter(obj => values.includes(obj.ObjectTypeId.toString()));
    replaceOptions('#ObjectIds', filteredObj);
};
objecTypesFilter.change(GetObjects);

let pgnumber = $('.pgnumber');
const WritePageNumber = async event =>{
    debugger;
    let pgNumber = event.target.attributes.pagenumber.value;
    $('#currentpg').val(pgNumber);
};
pgnumber.click(WritePageNumber);

let pgnumberbefore = $('.pgnumber-before');
const WritePageNumberBefore = async event =>{
    debugger;
    let pgNumber = event.target.attributes.pagenumber.value;
    $('#currentpg').val(1);
};
pgnumberbefore.click(WritePageNumberBefore);

let pgnumbernext = $('.pgnumber-next');
const WritePageNumberAfter = async event =>{
    debugger;
    let pgNumber = event.target.attributes.pagenumber.value;
    $('#currentpg').val(pgNumber);
};
pgnumbernext.click(WritePageNumberAfter);

let resetpgn = $('.resetpgn');
const ResetPagination = async event =>{
    debugger;
    let pgNumber = 1
    $('#currentpg').val(pgNumber);
};
resetpgn.click(ResetPagination);