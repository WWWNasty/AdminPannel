function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const DraggableCard = props => {
  const [state, setState] = React.useState({
    checked: true,
    gilad: true,
    jason: false,
    antoine: false
  });

  const handleChange = event => {
    setState({ ...state,
      [event.target.name]: event.target.checked
    });
  };

  const removeFriend = index => () => {
    props.setIndexes(prevIndexes => [...prevIndexes.filter(item => item !== index)]);
    props.setCounter(prevCounter => prevCounter - 1);
  };

  const useStyles = makeStyles(theme => createStyles({
    root: {
      width: '80%'
    },
    heading: {
      fontSize: theme.typography.pxToRem(15),
      fontWeight: theme.typography.fontWeightRegular
    }
  }));
  const classes = useStyles();
  const {
    gilad,
    jason,
    antoine
  } = state;
  return /*#__PURE__*/React.createElement("div", {
    className: "mt-3 bg-light"
  }, /*#__PURE__*/React.createElement(ListItem, _extends({
    ContainerComponent: "li",
    ContainerProps: {
      ref: props.provided.innerRef
    }
  }, props.provided.draggableProps, props.provided.dragHandleProps, {
    style: getItemStyle(props.snapshot.isDragging, props.provided.draggableProps.style)
  }), /*#__PURE__*/React.createElement(ListItemIcon, null, /*#__PURE__*/React.createElement(Icon, null, "help")), /*#__PURE__*/React.createElement("div", {
    className: "col"
  }, /*#__PURE__*/React.createElement("div", {
    className: "row"
  }, /*#__PURE__*/React.createElement(ListItemText, {
    primary: "\u0412\u043E\u043F\u0440\u043E\u0441" //{props.item.primary}
    // secondary={props.item.secondary}

  })), /*#__PURE__*/React.createElement("div", {
    className: "row"
  }, /*#__PURE__*/React.createElement(TextField, {
    required: true,
    id: "standard-basic",
    label: "\u0422\u0435\u043A\u0441\u0442 \u0432\u043E\u043F\u0440\u043E\u0441\u0430"
  }), /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Switch, {
      checked: state.checked,
      onChange: handleChange,
      name: "checked",
      color: "primary"
    }),
    label: "\u041E\u0431\u044F\u0437\u0430\u0442\u0435\u043B\u044C\u043D\u044B\u0439 \u0432\u043E\u043F\u0440\u043E\u0441"
  })), /*#__PURE__*/React.createElement("div", {
    className: `${classes.root} mt-3`
  }, /*#__PURE__*/React.createElement(Accordion, null, /*#__PURE__*/React.createElement(AccordionSummary, {
    expandIcon: /*#__PURE__*/React.createElement(Icon, null, "expand_more"),
    "aria-controls": "panel1a-content",
    id: "panel1a-header"
  }, /*#__PURE__*/React.createElement(Typography, {
    className: classes.heading
  }, "\u041E\u0442\u0432\u0435\u0442\u044B")), /*#__PURE__*/React.createElement(AccordionDetails, null, /*#__PURE__*/React.createElement(Typography, null, "\u0442\u0443\u0442 \u0431\u0443\u0434\u0443\u0442 \u043E\u0442\u0432\u0435\u0442\u044B", /*#__PURE__*/React.createElement(FormGroup, null, /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Checkbox, {
      checked: gilad,
      onChange: handleChange,
      color: "primary",
      name: "gilad"
    }),
    label: "Gilad Gray"
  }), /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Checkbox, {
      checked: jason,
      onChange: handleChange,
      color: "primary",
      name: "jason"
    }),
    label: "Jason Killian"
  }), /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Checkbox, {
      checked: antoine,
      onChange: handleChange,
      color: "primary",
      name: "antoine"
    }),
    label: "Antoine Llorca"
  }))))))), /*#__PURE__*/React.createElement(ListItemSecondaryAction, null, /*#__PURE__*/React.createElement(IconButton, {
    onClick: removeFriend(props.index)
  }, /*#__PURE__*/React.createElement(Icon, null, "delete")))));
};
//# sourceMappingURL=DraggableCard.js.map