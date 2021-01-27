declare const MaterialUI;
declare const ReactBeautifulDnd;
declare const ReactHookForm;
declare const yup;


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
    Snackbar
} = MaterialUI;

const {DragDropContext, Draggable, Droppable} = ReactBeautifulDnd;
const {useState} = React;
const {useForm, Controller, useFormContext, FormProvider} = ReactHookForm;


console.log(useFormContext);
console.log(FormProvider);

const useStyles = makeStyles((theme) => ({
    root: {
        width: '95%',
    },
    button: {
        marginRight: theme.spacing(1),
    },
    backButton: {
        marginRight: theme.spacing(1),
    },
    completed: {
        display: 'inline-block',
    },
    instructions: {
        marginTop: theme.spacing(1),
        marginBottom: theme.spacing(1),
    },
}));

function getSteps() {
    return ['Выбор типа объекта', 'Выбор объекта', 'Создание анкеты'];
}

function getStepContent(step: number, form: any) {
    const [objectTypes, setObjectTypes] = React.useState<SelectOption[]>([]);
    const [companies, setCompanies] = React.useState<SelectOption[]>([]);
    const [selectableAnswersLists, setSelectableAnswersLists] = React.useState<SelectOption[]>([]);
    const [questionaryInputFieldTypes, setQuestionaryInputFieldTypes] = React.useState<QuestionaryInputFieldTypes[]>([]);
    const [selectableAnswers, setSelectableAnswers] = React.useState<SelectOption[]>([]);

    React.useEffect(() => {
        (async () => {
            const response = await fetch("/api/QuestionaryApi", {
                method: "Get",
                headers: {"Accept": "application/json"},
                credentials: "include"
            });

            if (response.ok === true) {
                const selectOptions = await response.json();

                let objTypes: SelectOption[] = selectOptions.questionaryObjectTypes;
                setObjectTypes(objTypes);

                let companies: SelectOption[] = selectOptions.applicationCompanies.map(company => ({
                    id: company.companyId,
                    name: company.companyName
                }));
                setCompanies(companies);

                let answersList: SelectOption[] = selectOptions.selectableAnswersLists;
                setSelectableAnswersLists(answersList);

                let inputFieldTypes: QuestionaryInputFieldTypes[] = selectOptions.questionaryInputFieldTypes;
                setQuestionaryInputFieldTypes(inputFieldTypes);

                let answers: SelectableAnswers[] = selectOptions.selectableAnswers.map(answer => ({
                    name: answer.answerText,
                    id: answer.id,
                    selectableAnswersListId: answer.selectableAnswersListId
                }));
                console.log("ответы" + answers);

                setSelectableAnswers(answers);
            }
        })()
    }, []);

    const selectedObjectTypeId = form.watch('objectTypeId');
    const selectedObjectype = objectTypes.find(ot => ot.id == selectedObjectTypeId)

    switch (step) {
        case 0:
            return <FirstStep
                setObjectTypes={setObjectTypes}
                selectOptionsTypes={objectTypes}
                selectOptionsСompanies={companies}
            />;
        case 1:
            return <SecondStep
                form={form}
                selectOptions={objectTypes}
                selectedObjectype={selectedObjectype}
            />;
        case 2:
            return <ThirdStep
                form={form}
                selectableAnswersLists={selectableAnswersLists}
                questionaryInputFieldTypes={questionaryInputFieldTypes}
                selectableAnswers={selectableAnswers}
            />;
        default:
            return 'Unknown step';
    }
}

// function HorizontalNonLinearStepper() {
//     const classes = useStyles();
//     const [activeStep, setActiveStep] = useState(0);
//     const [completed, setCompleted] = useState({});
//     const steps = getSteps();
//
//     const totalSteps = () => {
//         return steps.length;
//     };
//
//     const completedSteps = () => {
//         return Object.keys(completed).length;
//     };
//
//     const isLastStep = () => {
//         return activeStep === totalSteps() - 1;
//     };
//
//     const allStepsCompleted = () => {
//         return completedSteps() === totalSteps();
//     };
//
//     const handleNext = () => {
//         const newActiveStep =
//             isLastStep() && !allStepsCompleted()
//                 ? // It's the last step, but not all steps have been completed,
//                   // find the first step that has been completed
//                 steps.findIndex((step, i) => !(i in completed))
//                 : activeStep + 1;
//         setActiveStep(newActiveStep);
//     };
//
//     const handleBack = () => {
//         setActiveStep((prevActiveStep) => prevActiveStep - 1);
//     };
//
//     const handleStep = (step) => () => {
//         setActiveStep(step);
//     };
//
//     const handleComplete = () => {
//         const newCompleted = completed;
//         newCompleted[activeStep] = true;
//         setCompleted(newCompleted);
//         handleNext();
//     };
//
//     const handleReset = () => {
//         setActiveStep(0);
//         setCompleted({});
//     };
//
//     return (
//         <div className={classes.root}>
//             <Stepper nonLinear activeStep={activeStep}>
//                 {steps.map((label, index) => (
//                     <Step key={label}>
//                         <StepButton onClick={handleStep(index)} completed={completed[index]}>
//                             {label}
//                         </StepButton>
//                     </Step>
//                 ))}
//             </Stepper>
//             <div>
//                 {allStepsCompleted() ? (
//                     <div>
//                         <Typography className={classes.instructions}>
//                             All steps completed - you&apos;re finished
//                         </Typography>
//                         <Button onClick={handleReset}>Reset</Button>
//                     </div>
//                 ) : (
//                     <div>
//                         <Typography className={classes.instructions}>{getStepContent(activeStep)}</Typography>
//                         <div>
//                             <Button disabled={activeStep === 0} onClick={handleBack} className={classes.button} variant="contained">
//                                 Назад
//                             </Button>
//                             <Button
//                                 variant="contained"
//                                 color="primary"
//                                 onClick={handleNext}
//                                 className={classes.button}
//                             >
//                                 Вперед
//                             </Button>
//                             {activeStep !== steps.length &&
//                             (completed[activeStep] ? (
//                                 <Typography variant="caption" className={classes.completed}>
//                                     Шаг {activeStep + 1} уже выполнен
//                                 </Typography>
//                             ) : (
//                                 <Button variant="contained" color="primary" onClick={handleComplete}>
//                                     {completedSteps() === totalSteps() - 1 ? 'Завершить' : 'Выполнить шаг'}
//                                 </Button>
//                             ))}
//                         </div>
//                     </div>
//                 )}
//             </div>
//         </div>
//     );
// }

//альтернативный степер без возможности свободного перехода по вкладкам с работой валидации
function HorizontalLabelPositionBelowStepper(props) {
    const classes = useStyles();
    const [activeStep, setActiveStep] = React.useState(0);
    const steps = getSteps();
    const form = useFormContext();
    // const {register} = form;

    const handleNext = () => {
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleReset = () => {
        setActiveStep(0);
    };

    return (
        <div className={classes.root}>
            <Stepper activeStep={activeStep} alternativeLabel>
                {steps.map((label) => (
                    <Step key={label}>
                        <StepLabel>{label}</StepLabel>
                    </Step>
                ))}
            </Stepper>
            <div>
                {activeStep === steps.length ? (
                    <div>
                        <Typography className={classes.instructions}>All steps completed</Typography>
                        <Button onClick={handleReset}>Reset</Button>
                    </div>
                ) : (
                    <div>
                        <Typography className={classes.instructions}>{getStepContent(activeStep, form)}</Typography>
                        <div>
                            <Button
                                disabled={activeStep === 0}
                                onClick={handleBack}
                                className={classes.backButton}
                                variant="contained"
                            >
                                Назад
                            </Button>
                            {activeStep === steps.length - 1 ?
                                <Button variant="contained" color="primary" onClick={handleNext}>
                                    Создать
                                </Button> :
                                <Button type="submit" variant="contained" color="primary" onClick={handleNext}>
                                    Вперед
                                </Button>}
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}

function App() {
    const form = useForm({shouldUnregister: false});
    const {register, handleSubmit} = form;
    const onSubmit = data => {
        console.log(data);
        const response = fetch("/api/ObjectTypeApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })
    };

    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)} autoComplete="off">
                <FormProvider {...form}>
                    <HorizontalLabelPositionBelowStepper/>
                </FormProvider>
            </form>
            <CloseAlertDialog/>
        </div>
    );
}

ReactDOM.render(<App/>, document.getElementById('reactRoot'));