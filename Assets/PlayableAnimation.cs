using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;


public class PlayableAnimation : MonoBehaviour
{
    public enum AniState
    { 
        Empty = -1,
        Idle = 0,
        Run = 1,
        Only_Attack = 2,
        Idle_Attack = 3,
        Run_Attack = 4,
    }

    public AniState state = AniState.Idle;

    public AnimationClip standClip;

    public AnimationClip attackClip;
    public AnimationClip runClip;
    //public AnimationClip swimmingClip;

    public AvatarMask footAvatarMask;
    public AvatarMask avatarMask;

    AnimationMixerPlayable m_upMixerPlayable;
    AnimationMixerPlayable m_downMixerPlayable;

    AnimationLayerMixerPlayable m_layerMixerPlayable;
    AnimationClipPlayable attackClipPlayable;
    private void Start()
    {
        PlayableGraph graph = PlayableGraph.Create("ChanPlayableGraph");
        var animationOutputPlayable = UnityEngine.Animations.AnimationPlayableOutput.Create(graph, "AnimationOutput", GetComponent<Animator>());

        graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        m_upMixerPlayable = AnimationMixerPlayable.Create(graph, 2);
        attackClipPlayable = AnimationClipPlayable.Create(graph, attackClip);
        var standClipClipPlayable = AnimationClipPlayable.Create(graph, standClip);
        m_upMixerPlayable.ConnectInput(0, attackClipPlayable, 0);
        m_upMixerPlayable.ConnectInput(1, standClipClipPlayable, 0);
        attackClipPlayable.SetSpeed(attackClip.length);


        m_downMixerPlayable = AnimationMixerPlayable.Create(graph, 1);
        var runClipPlayable = AnimationClipPlayable.Create(graph, runClip);
        m_downMixerPlayable.ConnectInput(0, runClipPlayable, 0);

        m_layerMixerPlayable = AnimationLayerMixerPlayable.Create(graph, 2);
        m_layerMixerPlayable.ConnectInput(0, m_upMixerPlayable, 0);
        m_layerMixerPlayable.ConnectInput(1, m_downMixerPlayable, 0);
        m_layerMixerPlayable.SetInputWeight(0, 1.0f);
        m_layerMixerPlayable.SetInputWeight(1, 1.0f);

        animationOutputPlayable.SetSourcePlayable(m_layerMixerPlayable);
        graph.Play();
    }

    private void Update()
    {
        switch (state)
        {
            case AniState.Idle:
                PlayIdle();
                break;
            case AniState.Run:
                PlayRun();
                break;
            case AniState.Idle_Attack:
                PlayIdleAttack();
                break;
            case AniState.Run_Attack:
                PlayRunAttack();
                break;
            case AniState.Only_Attack:
                PlayOnlyAttack();
                state = AniState.Empty;
                break;
            case AniState.Empty:
                break;
            default:
                PlayIdle();
                break;
        }
    }

    void PlayIdle()
    {
        m_layerMixerPlayable.SetInputWeight(0, 1.0f);
        m_layerMixerPlayable.SetInputWeight(1, 0);
        m_upMixerPlayable.SetInputWeight(0, 0);
        m_upMixerPlayable.SetInputWeight(1, 1.0f);
    }

    void PlayRun()
    {
        m_layerMixerPlayable.SetInputWeight(0, 0);
        m_layerMixerPlayable.SetInputWeight(1, 1.0f);
        m_downMixerPlayable.SetInputWeight(0, 1.0f);
        m_layerMixerPlayable.SetLayerMaskFromAvatarMask(1, avatarMask);
    }

    void PlayOnlyAttack()
    {
        m_layerMixerPlayable.SetInputWeight(0, 1.0f);
        m_layerMixerPlayable.SetInputWeight(1, 0);

        //attackClipPlayable.SetTime(0);
        m_upMixerPlayable.SetInputWeight(0, 1.0f);
        m_upMixerPlayable.SetInputWeight(1, 0);
    }

    void PlayIdleAttack()
    {
        PlayRunAttack();
    }

    void PlayRunAttack()
    {
        m_layerMixerPlayable.SetInputWeight(0, 1.0f);
        m_layerMixerPlayable.SetLayerMaskFromAvatarMask(1, footAvatarMask);
        m_layerMixerPlayable.SetInputWeight(1, 1.0f);

        //attackClipPlayable.SetTime(0);
        m_upMixerPlayable.SetInputWeight(0, 1.0f);
        m_upMixerPlayable.SetInputWeight(1, 0);
     
        m_downMixerPlayable.SetInputWeight(0, 1.0f);
    }
}
