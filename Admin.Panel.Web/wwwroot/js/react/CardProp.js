function CardProp(props) {
  const classes = useStyles();
  return /*#__PURE__*/React.createElement(Card, {
    className: `${classes.root} mt-3 mb-3 bg-light`
  }, /*#__PURE__*/React.createElement(CardContent, null, /*#__PURE__*/React.createElement(Typography, {
    className: classes.pos,
    color: "textSecondary"
  }, /*#__PURE__*/React.createElement("div", {
    className: 'd-flex'
  }, "C\u0432\u043E\u0439\u0441\u0442\u0432\u043E", /*#__PURE__*/React.createElement(IconButton, {
    "aria-label": "delete",
    className: `${classes.margin} ml-auto`,
    onClick: () => props.remove(props.index)
  }, /*#__PURE__*/React.createElement(Icon, null, "delete")))), /*#__PURE__*/React.createElement(Controller, {
    error: props.form.errors?.objectProperties?.[props.index]?.name?.type,
    as: TextField,
    name: `objectProperties[${props.index}].name`,
    className: "mr-3 col-md-3",
    defaultValue: "",
    required: true,
    control: props.form.control,
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435 \u0441\u0432\u043E\u0439\u0441\u0442\u0432\u0430",
    rules: {
      required: true,
      maxLength: {
        message: 'Максимально символов: 250',
        value: 250
      },
      validate: true
    },
    helperText: props.form.errors?.objectProperties?.[props.index]?.name?.message
  }), /*#__PURE__*/React.createElement(Controller, {
    error: props.form.errors?.objectProperties?.[props.index]?.nameInReport?.type,
    as: TextField,
    name: `objectProperties[${props.index}].nameInReport`,
    className: "mr-3 col-md-3",
    defaultValue: "",
    required: true,
    control: props.form.control,
    label: "\u041D\u0430\u0437\u0432\u0430\u043D\u0438\u0435 \u0441\u0432\u043E\u0439\u0441\u0442\u0432\u0430 \u0432 \u043E\u0442\u0447\u0435\u0442\u0435",
    rules: {
      required: true,
      maxLength: {
        message: 'Максимально символов: 250',
        value: 250
      },
      validate: true
    },
    helperText: props.form.errors?.objectProperties?.[props.index]?.nameInReport?.message
  }), /*#__PURE__*/React.createElement(FormControlLabel, {
    control: /*#__PURE__*/React.createElement(Switch, {
      className: "mr-3",
      name: `objectProperties[${props.index}].isUsedInReport`,
      color: "primary",
      inputRef: props.registerForm
    }),
    label: "\u042D\u0442\u043E \u043F\u043E\u043B\u0435 \u0438\u0441\u043F\u043E\u043B\u044C\u0437\u0443\u0435\u0442\u0441\u044F \u0432 \u043E\u0442\u0447\u0435\u0442\u0435"
  })));
}
//# sourceMappingURL=CardProp.js.map