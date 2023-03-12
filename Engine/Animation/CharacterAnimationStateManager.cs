using System;
using System.Collections.Generic;
using Engine.Animation.Base;
using Engine.Enum;

namespace Engine.Animation
{
    public class CharacterAnimationStateManager : BaseAnimationStateManager
    {
        private bool _statePlaysOnceOnChange;
        private bool _statePlaysOnLoop;
        private bool _stateAnimationIsPlayingOnce;
        private bool _stateAnimationIsLooping;
        private AnimationState _previousState = AnimationState.STATIC;
        private Dictionary<AnimationState, List<AnimationState>> _validNextStates = new Dictionary<AnimationState, List<AnimationState>>()
        {

            { AnimationState.IDLE , new List<AnimationState>(){ AnimationState.WALKING,
                                                                AnimationState.ROLLING,
                                                                AnimationState.RUNNING,
                                                                AnimationState.JUMPING
            }},
            { AnimationState.WALKING , new List<AnimationState>(){ AnimationState.IDLE,
                                                                   AnimationState.ROLLING,
                                                                   AnimationState.RUNNING,
                                                                   AnimationState.JUMPING
            }},
            { AnimationState.RUNNING , new List<AnimationState>(){ AnimationState.IDLE,
                                                                   AnimationState.ROLLING,
                                                                   AnimationState.WALKING,
                                                                   AnimationState.JUMPING
            }},
            { AnimationState.ROLLING , new List<AnimationState>(){ AnimationState.IDLE,
                                                                   AnimationState.WALKING,
                                                                   AnimationState.RUNNING,
                                                                   AnimationState.JUMPING
            }},
            { AnimationState.JUMPING , new List<AnimationState>(){ AnimationState.FALLING }},
            { AnimationState.FALLING , new List<AnimationState>(){ AnimationState.LANDING }},
            { AnimationState.LANDING , new List<AnimationState>(){ AnimationState.IDLE,
                                                                   AnimationState.WALKING,
                                                                   AnimationState.RUNNING,
                                                                   AnimationState.ROLLING,
            }}
        };

        public CharacterAnimationStateManager(AnimationState animationState) : base(animationState)
        {
            InitializeAnimationStateSettings();
        }

        private void InitializeAnimationStateSettings()
        {
            _statePlaysOnceOnChange = AnimationState == AnimationState.ROLLING ||
                AnimationState == AnimationState.JUMPING ||
                AnimationState == AnimationState.LANDING ? true : false;
            _statePlaysOnLoop = AnimationState == AnimationState.IDLE ||
               AnimationState == AnimationState.FALLING ? true : false;

            _stateAnimationIsPlayingOnce = _statePlaysOnceOnChange;
            _stateAnimationIsLooping = _statePlaysOnLoop;
        }

        public void UpdateAnimationState(AnimationState newState)
        {
            _previousState = AnimationState;
            AnimationState = newState;
            InitializeAnimationStateSettings();
        }

        public (bool update, AnimationState state) ValidateStateChange(AnimationState desiredState)
        {
            var currentState = AnimationState;
            if (_validNextStates.ContainsKey(currentState))
            {
                foreach(var possibleNextState in _validNextStates[currentState])
                {
                    if(desiredState == possibleNextState)
                    {
                        return (true, desiredState);
                    }
                }

                if(_validNextStates[currentState].Count > 0)
                {
                    return (true, _validNextStates[currentState][0]);
                }
            }
            return (false, desiredState);
        }

        public AnimationState PreviousState
        {
            get { return _previousState; }
        }

        public bool PlaysOnceOnChange
        {
            get { return _statePlaysOnceOnChange; }
        }

        public bool PlaysOnLoop
        {
            get { return _statePlaysOnLoop; }
        }

        public bool IsPlaying
        {
            get { return _stateAnimationIsPlayingOnce; }
        }

        public bool IsLooping
        {
            get { return _stateAnimationIsLooping; }
        }

        public void StartedPlaying()
        {
            _stateAnimationIsPlayingOnce = true;
        }

        public void StoppedPlaying()
        {
            _stateAnimationIsPlayingOnce = false;
        }

        public override double FrameDuration()
        {
            if (AnimationState == AnimationState.ROLLING) { return (18.0 / 60.0); }
            if (AnimationState == AnimationState.WALKING) { return (17.0 / 60.0); }
            if (AnimationState == AnimationState.RUNNING) { return (25.0 / 60.0); }
            if (AnimationState == AnimationState.IDLE) { return (10.0 / 60.0); }
            if (AnimationState == AnimationState.JUMPING) { return (30.0 / 60.0); }
            if (AnimationState == AnimationState.LANDING) { return (15.0 / 60.0); }
            return (15.0 / 60.0);
        }
    }
}

