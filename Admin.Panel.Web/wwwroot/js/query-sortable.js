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
            debugger;
            const container = $(item).parents("form");
            let inputs = $(container).children('input');
            let nameInputSkip = $(item).find(".chk-skip-input").attr("name");
            let nameInputUsed = $(item).find(".chk-used-input").attr("name");
            //let hiddens = $(inputs).find(`input[name="${nameInput}"]`);
            item.parentNode.removeChild(item); // remove sortable item
            $.each(inputs, function (index) {
                

                let nameHidden = $(this).attr("name");
                if (nameInputSkip === nameHidden || nameInputUsed === nameHidden) {
                    debugger;
                    $(this).remove();
                    console.log('хиддены удалены');
                }

            });
            debugger;
            let setsSimpleList = $('#simpleList').children('.voting-option-set');
            $(setsSimpleList).each((index, element) => {
                
                $(element).find('input').each((i, input) => {
                    const replaceIndexInName = (element) => {

                            const attribute = element.attributes.name?.value;
                            if(!attribute)
                                return;

                            const startIndex = attribute.indexOf('[');
                            const endIndex = attribute.indexOf(']');

                            if(startIndex === -1 || endIndex === -1)
                                return ;

                            const attributeStart = attribute.slice(0, startIndex + 1);
                            const attributeEnd = attribute.slice(endIndex);

                            element.attributes.name.value = attributeStart + index + attributeEnd;
                        }

                    //пересчет индекса в неймах у  элементов
                    replaceIndexInName(input);

                    //конец
                })
            })
            
            // for(let i = 0; i < inputs.length ;i++){
            //     let nameHidden = inputs[i].attr("name");
            //     if(nameHidden!== null){
            //         if (nameInputSkip === nameHidden || nameInputUsed === nameHidden) {
            //             inputs[i].remove();
            //         }
            //     }
            // }
            // inputs.each((_, input) => {
            //     let nameHidden = input.attr("name");
            //     if(nameHidden!== null){
            //         if (nameInputSkip === nameHidden || nameInputUsed === nameHidden) {
            //             input.remove();
            //         } 
            //     }
            //     //input.remove();
            // });
        }
    },
    //
    // onClone: function (evt) {
    //     var origEl = evt.item,
    //         cloneEl = evt.target;
    //
    //     if (Sortable.utils.is(cloneEl, ".js-add")) {
    //         // Click on add button
    //         origEl.parentNode.cloneNode(origEl); // add sortable item
    //     }
    // },
    onEnd: () => {
        console.log('перемещен элемент');
    },
    group: ".voting-option-set",
    store: {
        set: (sortable) => {
            const order = sortable.toArray();
            console.log(order);

            debugger;
            //откуда берем data-id
            let votingOptionSets = $('#simpleList').find(".voting-option-set").toArray();
            //куда записываем индекс
            //let inputs = $('#sequence-order').toArray();
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
//document.getElementById("sequence-order").value=selItem;
// Sortable(el,{
//     onClone: function (evt) {
//         var origEl = evt.item,
//             cloneEl = evt.clone;
//
//         if (Sortable.utils.is(cloneEl, ".js-add")) {
//             // Click on add button
//             origEl.parentNode.append(origEl); // add sortable item
//         }
//     },
//     // onClone: () => {
//     //     debugger;
//     //     console.log('nastya')
//     // },
// });
