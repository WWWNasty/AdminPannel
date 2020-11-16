let input = $('.selectpicker');

const Validate = async event => {
    const ides = event.target.value;
    let selects = $('.selectpicker');
    selects.each((_, select) => {
        if (select !== event.target) {
            const parent = $(select).parents('.bootstrap-select');
            let dropdown = $(parent).find('.dropdown-toggle');
            dropdown.prop('disabled', Boolean(ides));
        }
    })
};
input.change(Validate);

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