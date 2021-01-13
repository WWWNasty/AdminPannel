function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const DruggableComponent = () => {
  const [items, setItems] = useState([]);
  React.useEffect(() => {
    setItems(getItems(10));
  }, []);

  const onDragEnd = result => {
    // dropped outside the list
    if (!result.destination) {
      return;
    }

    const reorderedItems = reorder(items, result.source.index, result.destination.index);
    setItems(reorderedItems);
  };

  return /*#__PURE__*/React.createElement(DragDropContext, {
    onDragEnd: onDragEnd
  }, /*#__PURE__*/React.createElement(Droppable, {
    droppableId: "droppable"
  }, (provided, snapshot) => /*#__PURE__*/React.createElement(RootRef, {
    rootRef: provided.innerRef
  }, /*#__PURE__*/React.createElement(List, {
    style: getListStyle(snapshot.isDraggingOver)
  }, items.map((item, index) => /*#__PURE__*/React.createElement(Draggable, {
    key: item.id,
    draggableId: item.id,
    index: index
  }, (provided, snapshot) => /*#__PURE__*/React.createElement(ListItem, _extends({
    ContainerComponent: "li",
    ContainerProps: {
      ref: provided.innerRef
    }
  }, provided.draggableProps, provided.dragHandleProps, {
    style: getItemStyle(snapshot.isDragging, provided.draggableProps.style)
  }), /*#__PURE__*/React.createElement(ListItemIcon, null, /*#__PURE__*/React.createElement(Icon, null, "inbox")), /*#__PURE__*/React.createElement(ListItemText, {
    primary: item.primary,
    secondary: item.secondary
  }), /*#__PURE__*/React.createElement(ListItemSecondaryAction, null, /*#__PURE__*/React.createElement(IconButton, null, /*#__PURE__*/React.createElement(Icon, null, "edit")))))), provided.placeholder))));
}; // fake data generator


const getItems = count => Array.from({
  length: count
}, (v, k) => k).map(k => ({
  id: `item-${k}`,
  primary: `item ${k}`,
  secondary: k % 2 === 0 ? `Whatever for ${k}` : undefined
})); // a little function to help us with reordering the result


const reorder = (list, startIndex, endIndex) => {
  const result = Array.from(list);
  const [removed] = result.splice(startIndex, 1);
  result.splice(endIndex, 0, removed);
  return result;
};

const getItemStyle = (isDragging, draggableStyle) => ({ // styles we need to apply on draggables
  ...draggableStyle,
  ...(isDragging && {
    background: "rgb(235,235,235)"
  })
});

const getListStyle = isDraggingOver => ({//background: isDraggingOver ? 'lightblue' : 'lightgrey',
});
//# sourceMappingURL=DruggableComponent.js.map