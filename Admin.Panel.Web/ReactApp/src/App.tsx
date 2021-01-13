// import React from 'react';
// import { makeStyles } from '@material-ui/core/styles';
// import Stepper from '@material-ui/core/Stepper';
// import Step from '@material-ui/core/Step';
// import StepButton from '@material-ui/core/StepButton';
// import Button from '@material-ui/core/Button';
// import Typography from '@material-ui/core/Typography';

declare const MaterialUI;
declare const ReactBeautifulDnd;

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
    StepLabel
} = MaterialUI;

const { DragDropContext, Draggable, Droppable } = ReactBeautifulDnd;
const {useState} = React;

const useStyles = makeStyles((theme) => ({
    root: {
        width: '95%',
    },
    button: {
        marginRight: theme.spacing(1),
    },
    backButton:{
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

function getStepContent(step) {
    const [objectTypes, setObjectTypes] = React.useState<SelectOption[]>([]);
    const [selectedObjectType, setSelectedObjectType] = React.useState<number>(null);

    const [companies, setCompanies] =   React.useState<SelectOption[]>([]);
    const [selectedCompanies, setSelectedCompanies] = React.useState<number[]>([]);
    
    React.useEffect(() => {
        (async () => {
            const response = await fetch("/api/QuestionaryApi", {
                method: "Get",
                headers: {"Accept": "application/json"},
                credentials: "include"
            });
            debugger;
            // console.log(response);
            if (response.ok === true) {
                const selectOptions = await response.json();
                // console.log(selectOptions);
                let opt: SelectOption[] = selectOptions.questionaryObjectTypes;
                setObjectTypes(opt);
                let companies: SelectOption[] = selectOptions.applicationCompanies.map(company => ({id: company.companyId, name: company.companyName}));
                setCompanies(companies);
            }
        })()
    }, []);
    
    switch (step) {
        case 0:
            return <FirstStep selectOptionsTypes = {objectTypes} selectedValueTypes = {selectedObjectType} setSelectedValueTypes = {setSelectedObjectType}
                              selectOptionsСompanies = {companies} selectedValueСompanies = {selectedCompanies} setSelectedValueСompanies = {setSelectedCompanies}/>;
        case 1:
            return <SecondStep selectOptionsСompanies = {companies} selectedValueСompanies = {selectedCompanies} setSelectedValueСompanies = {setSelectedCompanies}/>;
        case 2:
            return <ThirdStep/>;
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
function HorizontalLabelPositionBelowStepper() {
    const classes = useStyles();
    const [activeStep, setActiveStep] = React.useState(0);
    const steps = getSteps();

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
                        <Typography className={classes.instructions}>{getStepContent(activeStep)}</Typography>
                        <div>
                            <Button
                                disabled={activeStep === 0}
                                onClick={handleBack}
                                className={classes.backButton}
                                variant="contained"
                            >
                                Назад
                            </Button>
                            <Button variant="contained" color="primary" onClick={handleNext}>
                                {activeStep === steps.length - 1 ? 'Создать' : 'Вперед'}
                            </Button>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
}

function App() {
    return (
        <div>
            <HorizontalLabelPositionBelowStepper/>
            <CloseAlertDialog/>
        </div>
        );
}

ReactDOM.render(<App/>, document.getElementById('reactRoot'));