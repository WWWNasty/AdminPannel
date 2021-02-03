interface QuestionaryInputFieldTypes extends SelectOption {
    selectableAnswersListId: number;
}
interface SelectableAnswers {
    id: number;
    name: string;
    selectableAnswersListId: number;
}
const DraggableCard = (props) =>{
    const form = useFormContext();
    const {register, control} = useFormContext();
    const useStyles = makeStyles((theme: any) =>
        createStyles({
            root: {
                width: '77%',
            },
            heading: {
                fontSize: theme.typography.pxToRem(15),
                fontWeight: theme.typography.fontWeightRegular,
            },
        }),
    );
    const classes = useStyles();
    const selectedSelectableAnswersListId = form.watch(`questionaryQuestions[${props.index}].selectableAnswersListId`);
    const availableSelectableAnswers = props.selectableAnswers.filter(answer => answer.selectableAnswersListId == selectedSelectableAnswersListId);
    const availableQuestionaryInputFieldTypeses = props.questionaryInputFieldTypes.filter(input => input.selectableAnswersListId == selectedSelectableAnswersListId);
    return (
        <div className="mt-3 bg-light">
            <ListItem
                ContainerComponent="li"
                ContainerProps={{ ref: props.provided.innerRef }}
                {...props.provided.draggableProps}
                {...props.provided.dragHandleProps}
                style={getItemStyle(
                    props.snapshot.isDragging,
                    props.provided.draggableProps.style
                )}
            >
                <ListItemIcon>
                    <Icon>help</Icon>
                </ListItemIcon>
                <div className='col'>
                    <div className='row'>
                        <ListItemText
                            primary= "Вопрос"
                        />
                    </div>
                    <div className='row'>
                        <Controller
                            as={TextField}
                            name={`questionaryQuestions[${props.index}].questionText`}
                            className="mr-3 col-md-9"
                            defaultValue={props.question.questionText}
                            required
                            control={control}
                            label="Текст вопроса"
                        />
                        {/*TODO fix state*/}
                        <FormControlLabel
                            control={
                                <Switch
                                    className="ml-5"
                                    name={`questionaryQuestions[${props.index}].canSkipQuestion`}
                                    color="primary"
                                    inputRef={register}
                                    defaultValue={props.question.canSkipQuestion}
                                />
                            } 
                            
                            label="Обязательный вопрос"
                        />
                        <MySelect required
                                  name={`questionaryQuestions[${props.index}].selectableAnswersListId`}
                                  selectOptions={props.selectableAnswersLists}
                                  nameSwlect="Варианты ответа"
                        />
                        <MySelect required
                                  name={`questionaryQuestions[${props.index}].questionaryInputFieldTypeId`}
                                  selectOptions={availableQuestionaryInputFieldTypeses}
                                  nameSwlect="Тип ввода"
                        />
                        <MySelect name={`questionaryQuestions[${props.index}].defaultAnswerId`}
                                  selectOptions={availableSelectableAnswers}
                                  nameSwlect="Ответ по умолчанию"
                        /> 
                        <input type="hidden"
                               ref={register}
                               name={`questionaryQuestions[${props.index}].sequenceOrder`}
                               value={props.index}
                        />
                    </div>
                    <div className={`${classes.root} mt-3`}>
                        <Accordion>
                            <AccordionSummary
                                expandIcon={<Icon>expand_more</Icon>}
                                aria-controls="panel1a-content"
                                id="panel1a-header"
                            >
                                <Typography className={classes.heading}>Ответы</Typography>
                            </AccordionSummary>
                            <AccordionDetails>
                                <Typography>
                                    Выберите ниже ответы, которым необходим комментарий
                                    <FormGroup>
                                        {availableSelectableAnswers?.map((item, index) =>
                                            <div>

                                                <FormControlLabel
                                                    control={
                                                        <Checkbox
                                                            name={`questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].isInvolvesComment`}
                                                            color="primary"
                                                            inputRef={register}
                                                            defaultValue=""
                                                        />
                                                    }
                                                    label={item.name}
                                                />
                                                <input type="hidden"
                                                       ref={register}
                                                       name={`questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].selectableAnswerId`}
                                                       value={item.id}
                                                />
                                            </div>
                                        )}
                                      
                                    </FormGroup>
                                </Typography>
                            </AccordionDetails>
                        </Accordion>
                    </div> 
                </div>
              
                <ListItemSecondaryAction>
                    <IconButton onClick={() => props.removeQuestion(props.index)}>
                        <Icon>delete</Icon>
                    </IconButton>
                </ListItemSecondaryAction>
            </ListItem>
        </div>
    );
}