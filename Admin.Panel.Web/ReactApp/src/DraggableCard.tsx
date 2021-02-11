interface QuestionaryInputFieldTypes extends SelectOption {
    selectableAnswersListId: number;
}

interface SelectableAnswers {
    id: number;
    name: string;
    selectableAnswersListId: number;
}

const DraggableCard = (props) => {
    const form = useFormContext();
    const {register, control, errors} = form;
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
    const onChange = () => {
        form.setValue(`questionaryQuestions[${props.index}].questionaryInputFieldTypeId`, null)
        form.setValue(`questionaryQuestions[${props.index}].defaultAnswerId`, null)
    };
    console.log(errors);
    return (
        <div className="mt-3 bg-light">
            <ListItem
                ContainerComponent="li"
                ContainerProps={{ref: props.provided.innerRef}}
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
                            primary="Вопрос"
                        />
                    </div>
                    <div className='row'>
                        <Controller
                            error={errors?.questionaryQuestions?.[props.index]?.questionText?.type}
                            as={TextField}
                            name={`questionaryQuestions[${props.index}].questionText`}
                            className="mr-3 col-md-9"
                            defaultValue={props.question.questionText}
                            required
                            id="standard-required"
                            control={control}
                            label="Текст вопроса"
                            rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                            helperText={Log(errors?.questionaryQuestions?.[props.index]?.questionText?.message)}
                        />
                        {/*TODO fix state*/}

                        <FormSwitch
                            name={`questionaryQuestions[${props.index}].canSkipQuestion`}
                            control={control}
                            label={"Обязательный вопрос"}
                            required={{message: '', value: true}}
                        />

                        <MySelect required={ {message: '', value: true} }
                                  onChange={onChange}
                                  error={Log(errors?.questionaryQuestions?.[props.index]?.selectableAnswersListId?.type)}
                                  name={`questionaryQuestions[${props.index}].selectableAnswersListId`}
                                  selectOptions={props.selectableAnswersLists}
                                  nameSwlect="Варианты ответа"
                        />
                        <MySelect required={ {message: '', value: true} }
                                  errorMessage={Log(errors?.questionaryQuestions?.[props.index]?.questionaryInputFieldTypeId?.message)}
                                  error={errors?.questionaryQuestions?.[props.index]?.questionaryInputFieldTypeId?.type}
                                  name={`questionaryQuestions[${props.index}].questionaryInputFieldTypeId`}
                                  selectOptions={availableQuestionaryInputFieldTypeses}
                                  nameSwlect="Тип ввода"
                        />
                        <MySelect
                            //error={errors?.questionaryQuestions?.[props.index]?.defaultAnswerId?.type}
                            name={`questionaryQuestions[${props.index}].defaultAnswerId`}
                            selectOptions={availableSelectableAnswers}
                            nameSwlect="Ответ по умолчанию"
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
                                                        <Controller
                                                            name={`questionaryQuestions[${props.index}].questionaryAnswerOptions[${index}].isInvolvesComment`}
                                                            control={control}
                                                            render={({onChange, value, ...props}) => (
                                                                <Checkbox
                                                                    {...props}
                                                                    checked={value}
                                                                    color="primary"
                                                                    onChange={(e) => onChange(e.target.checked)}
                                                                />
                                                            )}
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