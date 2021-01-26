const ReactHookFormSelect = ({
  name,
  label,
  control,
  defaultValue,
  children,
  ...props
}) => {
  const labelId = `${name}-label`;
  return /*#__PURE__*/React.createElement(FormControl, props, /*#__PURE__*/React.createElement(InputLabel, {
    id: labelId
  }, label), /*#__PURE__*/React.createElement(Controller, {
    as: /*#__PURE__*/React.createElement(Select, {
      labelId: labelId,
      label: label
    }, children),
    name: name,
    control: control,
    defaultValue: defaultValue
  }));
};

const MySelect = props => {
  const classes = useStyles();
  const {
    control
  } = useFormContext();
  return /*#__PURE__*/React.createElement(FormControl, {
    className: `${classes.formControl} col-md-3 mr-3`
  }, /*#__PURE__*/React.createElement(ReactHookFormSelect, {
    required: true,
    name: props.name,
    label: props.nameSwlect,
    defaultValue: props.selectedValue,
    className: classes.selectEmpty,
    control: control
  }, props.selectOptions?.map(item => /*#__PURE__*/React.createElement(MenuItem, {
    value: item.id
  }, item.name)) ?? []));
};
//# sourceMappingURL=MySelect.js.map