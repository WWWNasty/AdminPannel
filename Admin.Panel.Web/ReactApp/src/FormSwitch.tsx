const FormSwitch = (props) =>{
    
    return(
        <FormControlLabel
            control={
                <Controller
                    name={props.name}
                    control={props.control}
                    render={({onChange, value, ...props}) => (
                        <Switch
                            {...props}
                            className="ml-5"
                            checked={value}
                            color="primary"
                            onChange={(e) => onChange(e.target.checked)}
                        />
                    )}
                />
            }
            label={props.label}
        />
    )
}