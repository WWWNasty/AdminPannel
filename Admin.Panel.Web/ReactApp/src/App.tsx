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
const {useForm, Controller, useFormContext, FormProvider, useFieldArray} = ReactHookForm;


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

function getStepContent(step: number, form: any, questionary: any) {
    const [objectTypes, setObjectTypes] = React.useState<SelectOption[]>([]);
    const [companies, setCompanies] = React.useState<SelectOption[]>([]);
    const [selectableAnswersLists, setSelectableAnswersLists] = React.useState<SelectOption[]>([]);
    const [questionaryInputFieldTypes, setQuestionaryInputFieldTypes] = React.useState<QuestionaryInputFieldTypes[]>([]);
    const [selectableAnswers, setSelectableAnswers] = React.useState<SelectOption[]>([]);

    React.useEffect(() => {
        (async () => {

            const getSelectOptions = async () => {
                if (questionary)
                    return questionary;

                const response = await fetch("/api/QuestionaryApi", {
                    method: "Get",
                    headers: {"Accept": "application/json"},
                    credentials: "include"
                });

                if (response.ok) {
                    return await response.json();
                }
            }

            const selectOptions = await getSelectOptions();

            if(!selectOptions){
                //todo show a popup with error
                return;
            }
            
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

            setSelectableAnswers(answers);
            
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
    const {handleSubmit} = form;


    const handleNext = () => {
        const onSuccess = data => {
            form.clearErrors();

            setActiveStep((prevActiveStep) => prevActiveStep + 1);
        }
        handleSubmit(onSuccess)();
    };

    const handleBack = () => {
        const onSuccess = data => {
            form.clearErrors();

            setActiveStep((prevActiveStep) => prevActiveStep - 1);
        }
        handleSubmit(onSuccess)();
        
    };

    const handleReset = () => {
        setActiveStep(0);
    };
    const onSubmit = async data => {
        //edit mode change endpoint
        console.log(data);
        data.questionaryQuestions.forEach(question => question.questionaryAnswerOptions.forEach(option => option.selectableAnswerId = Number(option.selectableAnswerId)));

        const response = await fetch("/api/QuestionaryApi", {
            method: props.questionary ? "PUT" : "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })

        if (response.ok) {
            window.location = props.getAllRoute;
        }

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
                        <Typography
                            className={classes.instructions}>{getStepContent(activeStep, form, props.questionary)}</Typography>
                        <div>
                            <Button
                                disabled={activeStep === 0}
                                onClick={handleBack}
                                className={`${classes.backButton}`}
                                variant="contained"
                            >
                                Назад
                            </Button>
                            {activeStep === steps.length - 1 ?
                                <Button className="ml-50" onClick={handleSubmit(onSubmit)} variant="contained" color="primary">
                                    Сохранить
                                </Button> :
                                <Button className="ml-50" variant="contained" color="primary" onClick={handleNext}>
                                    Вперед
                                </Button>}
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}

function App(props: { questionary?: any, getAllRoute: string }) {

    console.log(props.questionary);

    const form = useForm({shouldUnregister: false, defaultValues: props.questionary});
    const {register, handleSubmit} = form;


    return (
        <div>
            <form autoComplete="off">
                <FormProvider {...form}>
                    <HorizontalLabelPositionBelowStepper 
                        questionary={props.questionary}
                        getAllRoute={props.getAllRoute}
                    />
                </FormProvider>
            </form>
            <CloseAlertDialog
                getAllRoute={props.getAllRoute}
            />
        </div>
    );
}
const Log = (a) => {
    console.log(a);
    return a;
}
const renderReact = (getAllRoute: string, questionary = undefined) => ReactDOM.render(<App getAllRoute={getAllRoute}
                                                                                           questionary={questionary}/>, document.getElementById('reactRoot'));