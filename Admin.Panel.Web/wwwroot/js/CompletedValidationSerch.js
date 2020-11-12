let input = $('.selectpicker');

const Validate = async event => {
    const ides = event.target.value;
    debugger;

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