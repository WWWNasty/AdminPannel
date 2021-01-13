const MySelect = props => {
  const classes = useStyles();

  const handleChange = event => {
    props.setSelectedValue(event.target.value);
  };

  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(FormControl, {
    required: true,
    className: `${classes.formControl} col-md-3`
  }, /*#__PURE__*/React.createElement(InputLabel, {
    id: "demo-simple-select-required-label"
  }, props.nameSwlect), /*#__PURE__*/React.createElement(Select, {
    labelId: "demo-simple-select-required-label",
    id: "demo-simple-select-required",
    value: props.selectedValue,
    onChange: handleChange,
    className: classes.selectEmpty
  }, props.selectOptions.map(item => /*#__PURE__*/React.createElement(MenuItem, {
    value: item.id
  }, item.name))), /*#__PURE__*/React.createElement(FormHelperText, null, "\u041E\u0431\u044F\u0437\u0430\u0442\u0435\u043B\u044C\u043D\u043E\u0435")));
};
//# sourceMappingURL=MySelect.js.map