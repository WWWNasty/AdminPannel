// import Sortable from 'jquery-sortablejs';
// import Sortable from "sortablejs";

const el = document.getElementById("simpleList");
const sortable = Sortable.create(el, {
    animation: 150,
    filter: ".js-remove",
    onFilter: function (evt) {
        var item = evt.item,
            ctrl = evt.target;
        if (Sortable.utils.is(ctrl, ".js-remove")) {
            // Click on remove button
            const container = $(item).parents("form");
            let inputs = $(container).children('input');
            let nameInputSkip = $(item).find(".chk-skip-input").attr("name");
            let nameInputUsed = $(item).find(".chk-used-input").attr("name");
            item.parentNode.removeChild(item); // remove sortable item
            let count = parseInt($('#voting-options-count').text());
            count--;
            console.log(count);
            $('#voting-options-count').text(count);
            $.each(inputs, function (index) {


                let nameHidden = $(this).attr("name");
                if (nameInputSkip === nameHidden || nameInputUsed === nameHidden) {
                    $(this).remove();
                    console.log('хиддены удалены');
                }

            });
            let setsSimpleList = $('#simpleList').children('.voting-option-set');
            $(setsSimpleList).each((index, element) => {

                $(element).find('input, select').each((i, input) => {
                    const replaceIndexInName = (element) => {

                        const attribute = element.attributes.name?.value;
                        if (!attribute)
                            return;

                        const startIndex = attribute.indexOf('[');
                        const endIndex = attribute.indexOf(']');

                        if (startIndex === -1 || endIndex === -1)
                            return;

                        const attributeStart = attribute.slice(0, startIndex + 1);
                        const attributeEnd = attribute.slice(endIndex);
                        if (element.attributes.itemIndex){
                            element.attributes.itemIndex.value = index;
                        }
                        element.attributes.name.value = attributeStart + index + attributeEnd;
                    }

                    //пересчет индекса в неймах у  элементов
                    replaceIndexInName(input);

                    //конец
                })
            })
        }
    },
    onEnd: () => {
        console.log('перемещен элемент');
    },
    group: ".voting-option-set",
    store: {
        set: (sortable) => {
            const order = sortable.toArray();
            console.log(order);

            //откуда берем data-id
            let votingOptionSets = $('#simpleList').find(".voting-option-set").toArray();
            //куда записываем индекс
            let inputs = $('#simpleList').find(".sequence-order").toArray();

            let index = 0;
            for (index; index < order.length; index++) {
                let idVoting = votingOptionSets[index].dataset.id;
                let idOrder = order[index];
                if (idOrder === idVoting) {
                    inputs[index].value = index;

                    console.log(index);
                } else {
                    console.log('значения data-id не совпали');
                }
            }
        }
    }
});
console.log(sortable);

