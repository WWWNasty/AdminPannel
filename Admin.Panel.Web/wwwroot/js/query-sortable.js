 // import Sortable from 'jquery-sortablejs';
 // import Sortable from "sortablejs";
 //import Sortable from "sortablejs";

var el = document.getElementById("simpleList");
Sortable.create(el, {
    animation: 150,
    
    onEnd: () => {
        console.log('перемещен элемент');
    },
    group: "voting-option-set",
    store: {
        set: (sortable) => {
            const order = sortable.toArray();
            console.log(order);
        }
    }
});
