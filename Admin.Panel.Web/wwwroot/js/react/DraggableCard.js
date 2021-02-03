function _extends() { _extends = Object.assign || function (target) { for (var i = 1; i < arguments.length; i++) { var source = arguments[i]; for (var key in source) { if (Object.prototype.hasOwnProperty.call(source, key)) { target[key] = source[key]; } } } return target; }; return _extends.apply(this, arguments); }

const DraggableCard = props => {
  const form = useFormContext();
  const {
    register,
    control
  } = useFormContext();
  const useStyles = makeStyles(theme => createStyles({
    root: {
      width: '77%'
    },
    heading: {
      fontSize: theme.typography.pxToRem(15),
      fontWeight: theme.typography.fontWeightRegular
    }
  }));
  const classes = useStyles();
  const selectedSelectableAnswersListId = form.watch(`questionaryQuestions[${props.index}].selectableAnswersListId`);
  const availableSelectableAnswers = props.selectableAnswers.filter(answer => answer.selectableAnswersListId == selectedSelectableAnswersListId);
  const availableQuestionaryInputFieldTypeses = props.questionaryInputFieldTypes.filter(input => input.selectableAnswersListId == selectedSelectableAnswersListId);
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
    primary: "\u0412\u043E\u043F\u0440\u043E\u0441"
  })), /*#__PURE__*/React.createElement("div", {
    className: "row"
  }, /*#__PURE__*/React.createElement(Controller, {
    as: TextField,
    name: `questionaryQuestions[${props.index}].questionText`,
    className: "mr-3 col-md-9",
    defaultValue: props.question.questionText,
    required: true,
    control: control,
    label: "\u0422\u0435\u043A\u0441\u0442 \u0432\u043E\u043F\u0440\u043E\u0441\u0430"
  }), /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Switch, {
      className: "ml-5",
      name: `questionaryQuestions[${props.index}].canSkipQuestion`,
      color: "primary",
      inputRef: register,
      defaultValue: props.question.canSkipQuestion
    }),
    label: "\u041E\u0431\u044F\u0437\u0430\u0442\u0435\u043B\u044C\u043D\u044B\u0439 \u0432\u043E\u043F\u0440\u043E\u0441"
  }), /*#__PURE__*/React.createElement(MySelect, {
    required: true,
    name: `questionaryQuestions[${props.index}].selectableAnswersListId`,
    selectOptions: props.selectableAnswersLists,
    nameSwlect: "\u0412\u0430\u0440\u0438\u0430\u043D\u0442\u044B \u043E\u0442\u0432\u0435\u0442\u0430"
  }), /*#__PURE__*/React.createElement(MySelect, {
    required: true,
    name: `questionaryQuestions[${props.index}].questionaryInputFieldTypeId`,
    selectOptions: availableQuestionaryInputFieldTypeses,
    nameSwlect: "\u0422\u0438\u043F \u0432\u0432\u043E\u0434\u0430"
  }), /*#__PURE__*/React.createElement(MySelect, {
    name: `questionaryQuestions[${props.index}].defaultAnswerId`,
    selectOptions: availableSelectableAnswers,
    nameSwlect: "\u041E\u0442\u0432\u0435\u0442 \u043F\u043E \u0443\u043C\u043E\u043B\u0447\u0430\u043D\u0438\u044E"
  }), /*#__PURE__*/React.createElement("input", {
    type: "hidden",
    ref: register,
    name: `questionaryQuestions[${props.index}].sequenceOrder`,
    value: props.index
  })), /*#__PURE__*/React.createElement("div", {
    className: `${classes.root} mt-3`
  }, /*#__PURE__*/React.createElement(Accordion, null, /*#__PURE__*/React.createElement(AccordionSummary, {
    expandIcon: /*#__PURE__*/React.createElement(Icon, null, "expand_more"),
    "aria-controls": "panel1a-content",
    id: "panel1a-header"
  }, /*#__PURE__*/React.createElement(Typography, {
    className: classes.heading
  }, "\u041E\u0442\u0432\u0435\u0442\u044B")), /*#__PURE__*/React.createElement(AccordionDetails, null, /*#__PURE__*/React.createElement(Typography, null, "\u0412\u044B\u0431\u0435\u0440\u0438\u0442\u0435 \u043D\u0438\u0436\u0435 \u043E\u0442\u0432\u0435\u0442\u044B, \u043A\u043E\u0442\u043E\u0440\u044B\u043C \u043D\u0435\u043E\u0431\u0445\u043E\u0434\u0438\u043C \u043A\u043E\u043C\u043C\u0435\u043D\u0442\u0430\u0440\u0438\u0439", /*#__PURE__*/React.createElement(FormGroup, null, availableSelectableAnswers?.map((item, index) => /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Checkbox, {
      name: `questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].isInvolvesComment`,
      color: "primary",
      inputRef: register,
      defaultValue: ""
    }),
    label: item.name
  }), /*#__PURE__*/React.createElement("input", {
    type: "hidden",
    ref: register,
    name: `questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].selectableAnswerId`,
    value: item.id
  }))))))))), /*#__PURE__*/React.createElement(ListItemSecondaryAction, null, /*#__PURE__*/React.createElement(IconButton, {
    onClick: () => props.removeQuestion(props.index)
  }, /*#__PURE__*/React.createElement(Icon, null, "delete")))));
};
//# sourceMappingURL=DraggableCard.js.map