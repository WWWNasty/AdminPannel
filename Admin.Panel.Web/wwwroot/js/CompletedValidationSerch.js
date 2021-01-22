function replaceOptions(selector, options) {
    const select = $(selector);

    select.empty();

    options.forEach(option => select.append(`<option value="${option.Id}">${option.Name}</option>`));

    select.selectpicker('refresh');
}

$(document).ready(async () => {
    const values = $('.companiesFilter').selectpicker('val');
    const typeObjs = $('.objecTypesFilter').selectpicker('val');
    const objs = $('.objectsFilter').selectpicker('val');

    const filteredTypesObj = formData.objectTypes.filter(type => values.includes(type.CompanyId.toString()));
    replaceOptions('#ObjectTypeIds', filteredTypesObj);

    let objTypeIds = filteredTypesObj.map(type => type.Id);
    const filteredObj = formData.objects.filter(obj => objTypeIds.includes(obj.ObjectTypeId));
    replaceOptions('#ObjectIds', filteredObj);

    $('.objecTypesFilter').selectpicker('val', typeObjs);
    $('.objectsFilter').selectpicker('val', objs);

    $('.selectpicker').selectpicker('render');
    $('.selectpicker').selectpicker('refresh');
})


let companiesFilter = $('.companiesFilter');
const GetSearchObjects = async event => {
    const values = $(event.target).selectpicker('val');
    const filteredTypesObj = formData.objectTypes.filter(type => values.includes(type.CompanyId.toString()));

    replaceOptions('#ObjectTypeIds', filteredTypesObj);

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
const WritePageNumber = async event => {
    let pgNumber = event.target.attributes.pagenumber.value;
    $('#currentpg').val(pgNumber);
};
pgnumber.click(WritePageNumber);

let pgnumberbefore = $('.pgnumber-before');
const WritePageNumberBefore = async event => {
    let pgNumber = event.target.attributes.pagenumber.value;
    $('#currentpg').val(1);
};
pgnumberbefore.click(WritePageNumberBefore);

let pgnumbernext = $('.pgnumber-next');
const WritePageNumberAfter = async event => {
    let pgNumber = event.target.attributes.pagenumber.value;
    $('#currentpg').val(pgNumber);
};
pgnumbernext.click(WritePageNumberAfter);

let resetpgn = $('.resetpgn');
const ResetPagination = async event => {
    let pgNumber = 1
    $('#currentpg').val(pgNumber);
};
resetpgn.click(ResetPagination);