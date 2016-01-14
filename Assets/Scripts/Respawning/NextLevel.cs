public class NextLevel : CheckTriggerEnterTag {

    protected override void OnTriggerEnterWithTag()
    {
        base.OnTriggerEnterWithTag();
        GetComponent<LoadScene>().LoadNextScene();
    }
}
