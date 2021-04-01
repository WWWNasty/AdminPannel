const MyMultipleSelect = props => {
  const form = useFormContext();

  function getStyles(name, personName, theme) {
    return {
      fontWeight: personName.indexOf(name) === -1 ? theme.typography.fontWeightRegular : theme.typography.fontWeightMedium
    };
  }

  const useStyles = makeStyles(theme => ({
    formControl: {
      margin: theme.spacing(0) // minWidth: 120,
      // maxWidth: 300,

    },
    noLabel: {
      marginTop: theme.spacing(3)
    }
  }));
  const ITEM_HEIGHT = 48;
  const ITEM_PADDING_TOP = 8;
  const MenuProps = {
    PaperProps: {
      style: {
        maxHeight: 700 //ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
        // width: 250,

      }
    }
  };
  const classes = useStyles();
  const {
    control
  } = useFormContext();
  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(FormControl, {
    className: `${classes.formControl} col-md-3`
  }, /*#__PURE__*/React.createElement(ReactHookFormSelect, {
    labelId: "demo-mutiple-checkbox-label",
    id: "demo-mutiple-checkbox",
    multiple: true,
    validate: () => {
      const isValid = props.form.getValues('objectsIdToChangeType')?.length > 0;
      const type = 'oneOrMoreRequired';
      if (!isValid) props.form.setError('objectsIdToChangeType', {
        type,
        message: 'Выберите объекты для анкеты!'
      });
      return isValid;
    },
    name: "objectsIdToChangeType",
    label: props.selectName,
    defaultValue: [],
    className: classes.selectEmpty,
    control: control,
    input: /*#__PURE__*/React.createElement(Input, null),
    selectOptions: props.selectOptions,
    renderValue: selected => props.selectOptions.flatMap(option => option.questionaryObjects).filter(option => option['isUsed'] = true).filter(option => selected.indexOf(option?.id) > -1).map(option => option.name).join(', '),
    MenuProps: MenuProps
  }, props.selectOptions?.map(item => [item.questionaryObjects?.length ? /*#__PURE__*/React.createElement(ListSubheader, null, item.name) : null, item.questionaryObjects?.map(object => /*#__PURE__*/React.createElement(MenuItem, {
    key: object.id,
    value: object.id
  }, /*#__PURE__*/React.createElement(Checkbox, {
    color: "primary",
    checked: form.watch('objectsIdToChangeType')?.indexOf(object.id) > -1
  }), /*#__PURE__*/React.createElement(ListItemText, {
    primary: object.name
  })))]))));
};
//# sourceMappingURL=MyMultipleSelect.js.map