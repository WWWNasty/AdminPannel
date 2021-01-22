const DraggableComponent = (props) => {
    const [items, setItems] = useState([]);

    // React.useEffect(() => {
    //     setItems(getItems(3))
    // },[])

    const [indexes, setIndexes] = React.useState([]);
    const [counter, setCounter] = React.useState(0);
    
    const addFriend = () => {
        setIndexes(prevIndexes => [...prevIndexes, counter]);
        setCounter(prevCounter => prevCounter + 1);
    };
    const onDragEnd = (result) => {
        // dropped outside the list
        if (!result.destination) {
            return;
        }

        const reorderedItems = reorder(
            items,
            result.source.index,
            result.destination.index
        );

        setItems(reorderedItems);
    }

    return (
        <div>
            <DragDropContext onDragEnd={onDragEnd}>
                <Droppable droppableId="droppable">
                    {(provided, snapshot) => (
                        <RootRef rootRef={provided.innerRef}>
                            <List style={getListStyle(snapshot.isDraggingOver)}>
                                {items.map((item, index) => (
                                    <Draggable key={item.id} draggableId={item.id} index={index}>
                                        {(provided, snapshot) => (
                                            {this:indexes.map(index => {
                                                        return(
                                                            <DraggableCard provided={provided} snapshot={snapshot} item={item}
                                                                              index={index} setIndexes={setIndexes}
                                                                              setCounter={setCounter}/>
                                                        );
                                                    }
                                                )}
                                            
                                        )}
                                    </Draggable>
                                ))}
                                {provided.placeholder}
                            </List>
                        </RootRef>
                    )}
                </Droppable>
            </DragDropContext>
            <div>
                <IconButton onClick={addFriend} color="primary" aria-label="add" className="mt-50 mb-50 ml-50">
                    <Icon>add</Icon>
                </IconButton>
            </div>
        </div>

    );
};


// fake data generator
// const getItems = count =>
//     Array.from({ length: count }, (v, k) => k).map(k => ({
//         id: `item-${k}`,
//         primary: `item ${k}`,
//         secondary: k % 2 === 0 ? `Whatever for ${k}` : undefined
//     }));

// a little function to help us with reordering the result
const reorder = (list, startIndex, endIndex) => {
    const result = Array.from(list);
    const [removed] = result.splice(startIndex, 1);
    result.splice(endIndex, 0, removed);

    return result;
};

const getItemStyle = (isDragging, draggableStyle) => ({
    // styles we need to apply on draggables
    ...draggableStyle,

    ...(isDragging && {
        background: "rgb(235,235,235)"
    })
});

const getListStyle = isDraggingOver => ({
    //background: isDraggingOver ? 'lightblue' : 'lightgrey'
});