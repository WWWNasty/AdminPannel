const {
  colors,
  CssBaseline,
  ThemeProvider,
  Typography,
  Container,
  makeStyles,
  createMuiTheme,
  Box,
  SvgIcon,
  Link,
  Stepper,
  Step,
  StepButton,
  Button,
  InputLabel,
  FormControl,
  FormHelperText,
  Select,
  MenuItem,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  Input,
  Chip,
  Checkbox,
  ListItemText,
  ListSubheader,
  DialogActions,
  Card,
  CardContent,
  CardActions,
  FormControlLabel,
  Icon,
  List,
  ListItem,
  ListItemIcon,
  IconButton,
  ListItemSecondaryAction,
  RootRef,
  Switch,
  Accordion,
  AccordionSummary,
  AccordionDetails,
  Theme,
  createStyles,
  FormGroup,
  Slide,
  StepLabel,
  Collapse,
  Snackbar,
  Tooltip
} = MaterialUI;
const {
  DragDropContext,
  Draggable,
  Droppable
} = ReactBeautifulDnd;
const {
  useState
} = React;
const {
  useForm,
  Controller,
  useFormContext,
  FormProvider,
  useFieldArray
} = ReactHookForm;
console.log(useFormContext);
console.log(FormProvider);
const useStyles = makeStyles(theme => ({
  root: {
    width: '95%'
  },
  button: {
    marginRight: theme.spacing(1)
  },
  backButton: {
    marginRight: theme.spacing(1)
  },
  completed: {
    display: 'inline-block'
  },
  instructions: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1)
  }
}));

function getSteps() {
  return ['Выбор типа объекта', 'Выбор объекта', 'Создание анкеты'];
}

function getStepContent(step, form, questionary, getAllRoute, objectTypes, setObjectTypes, companies, selectableAnswersLists, questionaryInputFieldTypes, selectableAnswers) {
  // const [objectTypes, setObjectTypes] = React.useState<SelectOption[]>([]);
  // const [companies, setCompanies] = React.useState<SelectOption[]>([]);
  // const [selectableAnswersLists, setSelectableAnswersLists] = React.useState<SelectOption[]>([]);
  // const [questionaryInputFieldTypes, setQuestionaryInputFieldTypes] = React.useState<QuestionaryInputFieldTypes[]>([]);
  // const [selectableAnswers, setSelectableAnswers] = React.useState<SelectOption[]>([]);
  //
  // React.useEffect(() => {
  //     (async () => {
  //
  //         const getSelectOptions = async () => {
  //             if (questionary)
  //                 return questionary;
  //            
  //             const basePath = getBasePath(getAllRoute);
  //            
  //             const response = await fetch(basePath + "/api/QuestionaryApi", {
  //                 method: "Get",
  //                 headers: {"Accept": "application/json"},
  //                 credentials: "include"
  //             });
  //
  //             if (response.ok) {
  //                 return await response.json();
  //             }
  //         }
  //
  //         const selectOptions = await getSelectOptions();
  //
  //         if(!selectOptions){
  //             //todo show a popup with error
  //             return;
  //         }
  //        
  //         let objTypes: SelectOption[] = selectOptions.questionaryObjectTypes;
  //         setObjectTypes(objTypes);
  //
  //         let companies: SelectOption[] = selectOptions.applicationCompanies.map(company => ({
  //             id: company.companyId,
  //             name: company.companyName
  //         }));
  //         setCompanies(companies);
  //
  //         let answersList: SelectOption[] = selectOptions.selectableAnswersLists;
  //         setSelectableAnswersLists(answersList);
  //
  //         let inputFieldTypes: QuestionaryInputFieldTypes[] = selectOptions.questionaryInputFieldTypes;
  //         setQuestionaryInputFieldTypes(inputFieldTypes);
  //
  //         let answers: SelectableAnswers[] = selectOptions.selectableAnswers.map(answer => ({
  //             name: answer.answerText,
  //             id: answer.id,
  //             selectableAnswersListId: answer.selectableAnswersListId
  //         }));
  //
  //         setSelectableAnswers(answers);
  //         console.log("3333",selectOptions);
  //
  //     })()
  // }, []);
  const selectedObjectTypeId = form.watch('objectTypeId');
  const selectedObjectype = objectTypes.find(ot => ot.id == selectedObjectTypeId);

  switch (step) {
    case 0:
      return /*#__PURE__*/React.createElement(FirstStep, {
        setObjectTypes: setObjectTypes,
        selectOptionsTypes: objectTypes,
        selectOptionsСompanies: companies
      });

    case 1:
      return /*#__PURE__*/React.createElement(SecondStep, {
        form: form,
        selectOptions: objectTypes,
        selectedObjectype: selectedObjectype
      });

    case 2:
      return /*#__PURE__*/React.createElement(ThirdStep, {
        form: form,
        selectableAnswersLists: selectableAnswersLists,
        questionaryInputFieldTypes: questionaryInputFieldTypes,
        selectableAnswers: selectableAnswers
      });

    default:
      return 'Unknown step';
  }
}

function getBasePath(getAllRoute) {
  const allRouteParts = getAllRoute.split('/');
  const basePath = allRouteParts.slice(0, allRouteParts.length - 2).join('/').trim();
  return basePath;
}

function HorizontalLabelPositionBelowStepper(props) {
  const classes = useStyles();
  const [activeStep, setActiveStep] = React.useState(0);
  const steps = getSteps();
  const form = useFormContext();
  const {
    handleSubmit
  } = form;

  const handleNext = () => {
    const onSuccess = data => {
      form.clearErrors();
      setActiveStep(prevActiveStep => prevActiveStep + 1);
    };

    handleSubmit(onSuccess)();
  };

  const handleBack = () => {
    const onSuccess = data => {
      form.clearErrors();
      setActiveStep(prevActiveStep => prevActiveStep - 1);
    };

    handleSubmit(onSuccess)();
  };

  const handleReset = () => {
    setActiveStep(0);
  };

  const onSubmit = async data => {
    //edit mode change endpoint
    console.log(data);
    data.questionaryQuestions.forEach(question => question.questionaryAnswerOptions.forEach(option => option.selectableAnswerId = Number(option.selectableAnswerId)));
    const basePath = getBasePath(props.getAllRoute);
    const response = await fetch(basePath + "/api/QuestionaryApi", {
      method: props.questionary ? "PUT" : "POST",
      headers: {
        "Content-Type": "application/json"
      },
      credentials: "include",
      body: JSON.stringify(data)
    });

    if (response.ok) {
      window.location = props.getAllRoute;
    }
  };

  const companyId = form.watch('companyId');
  const company = props.companies?.find(company => company.id.toString() == companyId);
  const objectTypeId = form.watch('objectTypeId');
  const objectType = props.objectTypes?.find(o => o.id == objectTypeId);
  const objectsIdToChangeType = form.watch('objectsIdToChangeType');
  return /*#__PURE__*/React.createElement("div", {
    className: classes.root
  }, /*#__PURE__*/React.createElement("div", {
    className: "d-flex justify-content-center"
  }, company?.name && /*#__PURE__*/React.createElement("div", {
    className: "d-flex justify-content-start col-3 text-truncate mr-1 "
  }, /*#__PURE__*/React.createElement("h6", {
    style: {
      color: '#3f51b5'
    }
  }, "\u041A\u043E\u043C\u043F\u0430\u043D\u0438\u044F: "), "\xA0", /*#__PURE__*/React.createElement(Tooltip, {
    title: company?.name,
    placement: "bottom-start"
  }, /*#__PURE__*/React.createElement("h6", {
    className: "font-weight-light",
    style: {
      color: '#3f51b5'
    }
  }, " ", company?.name))), objectType?.name && /*#__PURE__*/React.createElement("div", {
    className: "d-flex justify-content-start col-3 text-truncate mr-1"
  }, /*#__PURE__*/React.createElement("h6", {
    style: {
      color: '#3f51b5'
    }
  }, "\u0422\u0438\u043F \u043E\u0431\u044A\u0435\u043A\u0442\u0430: "), "\xA0", /*#__PURE__*/React.createElement(Tooltip, {
    title: objectType?.name,
    placement: "bottom-start"
  }, /*#__PURE__*/React.createElement("h6", {
    className: "font-weight-light",
    style: {
      color: '#3f51b5'
    }
  }, " ", objectType?.name))), objectsIdToChangeType?.length ? /*#__PURE__*/React.createElement("div", {
    className: "d-flex justify-content-start col-2 text-truncate"
  }, /*#__PURE__*/React.createElement("h6", {
    style: {
      color: '#3f51b5'
    }
  }, "\u0412\u044B\u0431\u0440\u0430\u043D\u043E \u043E\u0431\u044A\u0435\u043A\u0442\u043E\u0432: "), "\xA0", /*#__PURE__*/React.createElement("h6", {
    className: "font-weight-light",
    style: {
      color: '#3f51b5'
    }
  }, " ", objectsIdToChangeType?.length)) : null), /*#__PURE__*/React.createElement(Stepper, {
    activeStep: activeStep,
    alternativeLabel: true
  }, steps.map(label => /*#__PURE__*/React.createElement(Step, {
    key: label
  }, /*#__PURE__*/React.createElement(StepLabel, null, label)))), /*#__PURE__*/React.createElement("div", null, activeStep === steps.length ? /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Typography, {
    className: classes.instructions
  }, "All steps completed"), /*#__PURE__*/React.createElement(Button, {
    onClick: handleReset
  }, "Reset")) : /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Typography, {
    className: classes.instructions
  }, getStepContent(activeStep, form, props.questionary, props.getAllRoute, props.objectTypes, props.setObjectTypes, props.companies, props.selectableAnswersLists, props.questionaryInputFieldTypes, props.selectableAnswers)), /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement(Button, {
    disabled: activeStep === 0,
    onClick: handleBack,
    className: `${classes.backButton}`,
    variant: "contained"
  }, "\u041D\u0430\u0437\u0430\u0434"), activeStep === steps.length - 1 ? /*#__PURE__*/React.createElement(Button, {
    className: "ml-50",
    onClick: handleSubmit(onSubmit),
    variant: "contained",
    color: "primary"
  }, "\u0421\u043E\u0445\u0440\u0430\u043D\u0438\u0442\u044C") : /*#__PURE__*/React.createElement(Button, {
    className: "ml-50",
    variant: "contained",
    color: "primary",
    onClick: handleNext
  }, "\u0412\u043F\u0435\u0440\u0435\u0434")))));
}

function App(props) {
  const form = useForm({
    shouldUnregister: false,
    defaultValues: props.questionary
  });
  const {
    register,
    handleSubmit
  } = form;
  const [objectTypes, setObjectTypes] = React.useState([]);
  const [companies, setCompanies] = React.useState([]);
  const [selectableAnswersLists, setSelectableAnswersLists] = React.useState([]);
  const [questionaryInputFieldTypes, setQuestionaryInputFieldTypes] = React.useState([]);
  const [selectableAnswers, setSelectableAnswers] = React.useState([]);
  const [companyId, setCompanyId] = React.useState(0);
  React.useEffect(() => {
    (async () => {
      const getSelectOptions = async () => {
        if (props.questionary) return props.questionary;
        const basePath = getBasePath(props.getAllRoute);
        const response = await fetch(basePath + "/api/QuestionaryApi", {
          method: "Get",
          headers: {
            "Accept": "application/json"
          },
          credentials: "include"
        });

        if (response.ok) {
          return await response.json();
        }
      };

      const selectOptions = await getSelectOptions();

      if (!selectOptions) {
        //todo show a popup with error
        return;
      }

      let companyId = form.watch('companyId');
      setCompanyId(companyId);
      let objTypes = selectOptions.questionaryObjectTypes;
      setObjectTypes(objTypes);
      let companies = selectOptions.applicationCompanies.map(company => ({
        id: company.companyId,
        name: company.companyName
      }));
      setCompanies(companies);
      let answersList = selectOptions.selectableAnswersLists;
      setSelectableAnswersLists(answersList);
      let inputFieldTypes = selectOptions.questionaryInputFieldTypes;
      setQuestionaryInputFieldTypes(inputFieldTypes);
      let answers = selectOptions.selectableAnswers.map(answer => ({
        name: answer.answerText,
        id: answer.id,
        selectableAnswersListId: answer.selectableAnswersListId
      }));
      setSelectableAnswers(answers);
    })();
  }, []);
  return /*#__PURE__*/React.createElement("div", null, /*#__PURE__*/React.createElement("form", {
    autoComplete: "off"
  }, /*#__PURE__*/React.createElement(FormProvider, form, /*#__PURE__*/React.createElement(HorizontalLabelPositionBelowStepper, {
    questionary: props.questionary,
    getAllRoute: props.getAllRoute,
    objectTypes: objectTypes,
    setObjectTypes: setObjectTypes,
    companies: companies,
    selectableAnswersLists: selectableAnswersLists,
    questionaryInputFieldTypes: questionaryInputFieldTypes,
    selectableAnswers: selectableAnswers,
    companyId: companyId
  }))), /*#__PURE__*/React.createElement(CloseAlertDialog, {
    getAllRoute: props.getAllRoute
  }));
}

const Log = a => {
  console.log(a);
  return a;
};

const renderReact = (getAllRoute, questionary = undefined) => ReactDOM.render( /*#__PURE__*/React.createElement(App, {
  getAllRoute: getAllRoute,
  questionary: questionary
}), document.getElementById('reactRoot'));
//# sourceMappingURL=App.js.map