const DraggableComponent = (props) => {


    const form = useFormContext();


    const questionsFieldName = `questionaryQuestions`;

    const {remove, append, move, fields} = useFieldArray({control: form.control, name: questionsFieldName});

    const onDragEnd = (result) => {
        // dropped outside the list
        if (!result.destination) {
            return;
        }

        move(result.source.index, result.destination.index);
    }

    return (
        <div>
            <DragDropContext onDragEnd={onDragEnd}>
                <Droppable droppableId="droppable">
                    {(provided, snapshot) => (
                        <RootRef rootRef={provided.innerRef}>
                            <List style={getListStyle(snapshot.isDraggingOver)}>
                                {fields.map((question, index) =>
                                    <Draggable key={question.key} index={index} draggableId={question.key.toString()}>
                                        {(provided, snapshot) =>
                                            <DraggableCard
                                                question={question}
                                                form={form}
                                                selectableAnswersLists={props.selectableAnswersLists}
                                                questionaryInputFieldTypes={props.questionaryInputFieldTypes}
                                                selectableAnswers={props.selectableAnswers}
                                                provided={provided}
                                                removeQuestion={() => remove(index)}
                                                snapshot={snapshot}
                                                index={index}
                                            />
                                        }
                                    </Draggable>)
                                }
                                {provided.placeholder}
                            </List>
                        </RootRef>
                    )}
                </Droppable>
            </DragDropContext>
            <div>
                <IconButton 
                    onClick={() => append({
                        key: Math.random(),
                        questionText: '',
                        canSkipQuestion: false,
                        selectableAnswersListId: null,
                        questionaryInputFieldTypeId: null,
                        defaultAnswerId: null,
                        questionaryAnswerOptions: []
                    })
                } 
                    color="primary" 
                    aria-label="add" 
                    className="mt-50 mb-50 ml-50">
                    <Icon>add</Icon>
                </IconButton>
            </div>
        </div>
    );
};


// fake data generator
const getItems = count =>
    Array.from({length: count}, (v, k) => k).map(k => ({
        id: `item-${k}`,
        primary: `item ${k}`,
        secondary: k % 2 === 0 ? `Whatever for ${k}` : undefined
    }));

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