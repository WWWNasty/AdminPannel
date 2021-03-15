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

function getStepContent(step: number, 
                        form: any, 
                        questionary: any, 
                        getAllRoute: string, 
                        objectTypes, 
                        setObjectTypes, 
                        companies, 
                        selectableAnswersLists,
                        questionaryInputFieldTypes,
                        selectableAnswers) {
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

function getBasePath(getAllRoute: string) {
    const allRouteParts = getAllRoute.split('/');
    const basePath = allRouteParts.slice(0, allRouteParts.length - 2).join('/').trim();
    return basePath;
}

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

        const basePath = getBasePath(props.getAllRoute);

        const response = await fetch(basePath + "/api/QuestionaryApi", {
            method: props.questionary ? "PUT" : "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })

        if (response.ok) {
            window.location = props.getAllRoute;
        }

    };
    const companyId = form.watch('companyId');
    //const applicationCompanies = form.watch('applicationCompanies');
    const company: {id: number, name: string} = props.companies?.find(company => company.id.toString() == companyId);
    console.log("company00000", company);
    console.log("company00000", props.companies);

    const objectTypeId = form.watch('objectTypeId');
    //const questionaryObjectTypes = form.watch('questionaryObjectTypes');
    const objectType = props.objectTypes?.find(o => o.id == objectTypeId);
    //console.log(objectType?.name);

    const objectsIdToChangeType = form.watch('objectsIdToChangeType');
    return (
        <div className={classes.root}>
            <div>Компания: {company?.name}</div>
            <div> Тип объекта: {objectType?.name}</div>
            <div> Выбрано объектов: {objectsIdToChangeType?.length}</div>
            
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
                            className={classes.instructions}>{getStepContent(
                                activeStep, 
                            form, 
                            props.questionary, 
                            props.getAllRoute,
                            props.objectTypes,
                            props.setObjectTypes,
                            props.companies,
                            props.selectableAnswersLists,
                            props.questionaryInputFieldTypes,
                            props.selectableAnswers
                        )}</Typography>
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
interface Company {
    companyId: number;
    companyName: string;
}
function App(props: { questionary?: any, getAllRoute: string }) {
    
    const form = useForm({shouldUnregister: false, defaultValues: props.questionary});
    const {register, handleSubmit} = form;

    const [objectTypes, setObjectTypes] = React.useState<SelectOption[]>([]);
    const [companies, setCompanies] = React.useState<SelectOption[]>([]);
    const [selectableAnswersLists, setSelectableAnswersLists] = React.useState<SelectOption[]>([]);
    const [questionaryInputFieldTypes, setQuestionaryInputFieldTypes] = React.useState<QuestionaryInputFieldTypes[]>([]);
    const [selectableAnswers, setSelectableAnswers] = React.useState<SelectOption[]>([]);
    const [companyId, setCompanyId] = React.useState<number>(0);

    React.useEffect(() => {
        (async () => {

            const getSelectOptions = async () => {
                if (props.questionary)
                    return props.questionary;

                const basePath = getBasePath(props.getAllRoute);

                const response = await fetch(basePath + "/api/QuestionaryApi", {
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

            let companyId = form.watch('companyId');
            setCompanyId(companyId);

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
            console.log("3333",selectOptions);

        })()
    }, []);
    
    return (
        <div>
            <form autoComplete="off">
                <FormProvider {...form}>
                    {/*{company?.companyName && <div>Компания: {company.companyName}</div>}*/}
                    {/*{objectType.name && <div> Тип объекта: {objectType.name}</div>}*/}
                    {/*{objectsIdToChangeType && <div> Выбрано объектов: {objectsIdToChangeType.length}</div>}*/}
                    {/*<div>Компания: {company?.companyName}</div>*/}
                    {/*<div> Тип объекта: {objectType?.name}</div>*/}
                    {/*<div> Выбрано объектов: {objectsIdToChangeType?.length}</div>*/}
                    <HorizontalLabelPositionBelowStepper 
                        questionary={props.questionary}
                        getAllRoute={props.getAllRoute}
                        objectTypes={objectTypes}
                        setObjectTypes={setObjectTypes}
                        companies={companies}
                        selectableAnswersLists={selectableAnswersLists}
                        questionaryInputFieldTypes={questionaryInputFieldTypes}
                        selectableAnswers={selectableAnswers}
                        companyId={companyId}
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