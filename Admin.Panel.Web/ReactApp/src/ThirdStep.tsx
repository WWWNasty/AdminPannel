const ThirdStep = (props) => {
    const form = useFormContext();
    const {register, handleSubmit, control, errors} = form;
    const questionaryId = form.watch('id');
    const ifQuestionaryCurrentInCompany = form.watch('ifQuestionaryCurrentInCompany');
    const objectTypeId = form.watch('objectTypeId');
console.log("originObjectTypeId", props.originObjectTypeId, "objectTypeId", objectTypeId);

    return (
        <div>
            {questionaryId != null &&
                <div>
                    {(ifQuestionaryCurrentInCompany && props.originObjectTypeId == objectTypeId)?
                        <FormControlLabel disabled control={<Switch />} label="Анкета не активна" />:
                        <FormSwitch
                            name={`isUsed`}
                            control={control}
                            label={"Анкета активна"}
                        />
                    }
                </div>
                    
            }

            <Controller
                error={errors?.name?.type}
                as={TextField}
                autoFocus
                name="name"
                control={control}
                defaultValue=""
                required
                margin="dense"
                id="standard-required"
                label="Название анкеты"
                fullWidth={true}
                rules={{required: true, maxLength: {message:'Максимально символов: 250', value:250}, validate: true}}
                helperText={errors?.name?.message}
            />
            
            <DraggableComponent
                form={form}
                selectableAnswersLists={props.selectableAnswersLists}
                questionaryInputFieldTypes={props.questionaryInputFieldTypes}
                selectableAnswers={props.selectableAnswers}
            />
        </div>
    );
}
