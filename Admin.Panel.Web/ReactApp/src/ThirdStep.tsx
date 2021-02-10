const ThirdStep = (props) => {
    const form = useFormContext();
    console.log(form);
    const {register, handleSubmit, control, errors} = form;
    const questionaryId = form.watch('id');
    console.log(errors);
    return (
        <div>
            {questionaryId != null &&
            <FormSwitch
                name={`isUsed`}
                control={control}
                label={"Анкета активна"}
            />
            }
          
            <Controller
                error={errors.name?.type}
                as={TextField}
                autoFocus
                name="name"
                control={control}
                defaultValue=""
                required
                margin="dense"
                id="standard-required"
                label="Название"
                fullWidth={true}
                //required={ {message: '', value: true} }
                rules={{required: true, maxLength: 250, validate: true}}
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
