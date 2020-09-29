 // import Sortable from 'jquery-sortablejs';
 // import Sortable from "sortablejs";
 //import Sortable from "sortablejs";

var el = document.getElementById("simpleList");
Sortable.create(el, {
    animation: 150,
    filter: ".js-remove",
    onFilter: function (evt) {
        var item = evt.item,
            ctrl = evt.target;

        if (Sortable.utils.is(ctrl, ".js-remove")) {
            // Click on remove button
            item.parentNode.removeChild(item); // remove sortable item
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

            debugger;
            //откуда берем data-id
            let votingOptionSets = $('#simpleList').find(".voting-option-set").toArray();
            //куда записываем индекс
            //let inputs = $('#sequence-order').toArray();
            let inputs = $('#simpleList').find(".sequence-order").toArray();

            let index = 0;
            for(index; index < order.length; index++){
                let idVoting = votingOptionSets[index].dataset.id;
                let idOrder = order[index];
                if(idOrder === idVoting){
                    inputs[index].value = index; 
                    
                    console.log(index);
                }else{
                    console.log('значения data-id не совпали');
                }
            }
        }
    }
});

 //document.getElementById("sequence-order").value=selItem;
 
