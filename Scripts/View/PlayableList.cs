using System;
using UnityEngine;
using UnityEngine.Playables;

namespace kameffee.unity1week202104.View
{
    [Serializable]
    public class PlayableList
    {
        [SerializeField]
        private PlayableDirector intro;
        public PlayableDirector Intro => intro;

        [SerializeField]
        private PlayableDirector outro;
        public PlayableDirector Outro => outro;
    }
}